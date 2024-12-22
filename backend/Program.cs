global using FastEndpoints;
using System.Text.Json;
using Backend.Constants;
using Backend.Database;
using Backend.Database.Interceptors;
using Backend.Database.Seeders;
using Backend.Services;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var bld = WebApplication.CreateBuilder();
var config = bld.Configuration;
var connectionString = config.GetConnectionString("DefaultConnection");
var directory = config.GetValue<string>("Directory") ?? "directory";

// Configure services
bld.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
bld.Services.AddHttpContextAccessor();
bld.Services.AddSingleton<IStorageService, StorageService>();
bld.Services.AddSingleton<IUserService, UserService>();
bld.Services.AddSingleton<IEmailService, EmailService>();
bld.Services.AddScoped<AppDbInterceptor>();
bld.Services.AddScoped<IReferenceNumberService, ReferenceNumberService>();
bld.Services.AddSpaStaticFiles(o => o.RootPath = "dist");
bld.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(directory))
    .SetDefaultKeyLifetime(TimeSpan.FromDays(30));

bld.Services.AddAuthenticationCookie(validFor: TimeSpan.FromHours(8))
    .AddAuthenticationJwtBearer(s => s.SigningKey = config["JwtKey:Secret"])
    .AddAuthentication(o =>
    {
        o.DefaultScheme = AuthConstants.JwtOrCookie;
        o.DefaultAuthenticateScheme = AuthConstants.JwtOrCookie;
    })
    .AddPolicyScheme(
        AuthConstants.JwtOrCookie,
        AuthConstants.JwtOrCookie,
        o =>
        {
            o.ForwardDefaultSelector = ctx =>
            {
                if (
                    ctx.Request.Headers.TryGetValue(HeaderNames.Authorization, out var authHeader)
                    && authHeader.FirstOrDefault()?.StartsWith("Bearer ") is true
                )
                {
                    return JwtBearerDefaults.AuthenticationScheme;
                }
                return CookieAuthenticationDefaults.AuthenticationScheme;
            };
        }
    );
bld.Services.AddAuthorization().AddFastEndpoints();
var app = bld.Build();

// Run args seed the database
if (args.Length > 0 && args[0] == "seed")
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    Seeder.Seed(db);
    Environment.Exit(0);
}

app.UseRouting();
app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints(o =>
    {
        o.Endpoints.RoutePrefix = "api";
        o.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
app.UseEndpoints(_ => { });

// Spa fallback
app.UseSpaStaticFiles();
app.UseSpa(x =>
{
    if (!app.Environment.IsDevelopment())
        return;
    x.UseProxyToSpaDevelopmentServer("http://localhost:5901");
});

app.Run();
