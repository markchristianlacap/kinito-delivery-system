using System.Security.Claims;
using Backend.Database;
using Backend.Entities;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class JWTRefreshTokenService : RefreshTokenService<TokenRequest, TokenResponse>
{
    private readonly AppDbContext _db;

    public JWTRefreshTokenService(AppDbContext db, IConfiguration config)
    {
        Setup(o =>
        {
            o.TokenSigningKey = config["JWT:Secret"];
            o.AccessTokenValidity = TimeSpan.FromMinutes(30);
            o.RefreshTokenValidity = TimeSpan.FromHours(8);
            o.Endpoint("/auth/refresh-token", _ => { });
        });
        _db = db;
    }

    public override async Task PersistTokenAsync(TokenResponse response)
    {
        var userId = Guid.TryParse(response.UserId, out var id) ? id : Guid.Empty;
        if (userId == Guid.Empty)
        {
            ThrowError(x => x.UserId, "Invalid user");
        }
        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Token = response.RefreshToken,
            AccessToken = response.AccessToken,
            AccessExpiry = response.AccessExpiry,
            RefreshExpiry = response.RefreshExpiry,
        };
        await _db.RefreshTokens.AddAsync(refreshToken);
        await _db.SaveChangesAsync();
    }

    public override async Task RefreshRequestValidationAsync(TokenRequest req)
    {
        var userId = Guid.TryParse(req.UserId, out var id) ? id : Guid.Empty;
        if (userId == Guid.Empty)
        {
            ThrowError(x => x.UserId, "Invalid user");
        }
        var isValid = await _db.RefreshTokens.AnyAsync(x =>
            x.Token == req.RefreshToken && x.UserId == userId && x.RefreshExpiry >= DateTime.UtcNow
        );
        if (!isValid)
        {
            ThrowError(x => x.RefreshToken, "Invalid refresh token");
        }
    }

    public override async Task SetRenewalPrivilegesAsync(
        TokenRequest req,
        UserPrivileges privileges
    )
    {
        var userId = Guid.TryParse(req.UserId, out var id) ? id : Guid.Empty;
        if (userId == Guid.Empty)
        {
            ThrowError(x => x.UserId, "Invalid user");
        }
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId && x.IsActive);
        if (user == null)
        {
            ThrowError(x => x.UserId, "Invalid user");
        }
        privileges.Claims.Add(new(ClaimTypes.NameIdentifier, user.Id.ToString()));
        privileges.Roles.Add(user.Role.ToString());
    }
}
