using System.Security.Claims;
using Backend.Database;
using Backend.Services;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Login.Mobile;

public class Endpoint : Endpoint<LoginReq, TokenResponse>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Post("/mobile/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginReq req, CancellationToken ct)
    {
        var user = await Db.Users.FirstOrDefaultAsync(u => u.Email == req.Email, ct);
        if (user is null)
        {
            ThrowError(x => x.Email, "Email or password is invalid");
        }
        var correctPassword = BCrypt.Net.BCrypt.EnhancedVerify(req.Password, user.Password);
        if (!correctPassword)
        {
            ThrowError(x => x.Password, "Email or password is invalid");
        }
        var token = await CreateTokenWith<JWTRefreshTokenService>(
            user.Id.ToString(),
            u =>
            {
                u.Roles.Add(user.Role.ToString());
                u.Claims.Add(new(ClaimTypes.NameIdentifier, user.Id.ToString()));
            }
        );
    }
}
