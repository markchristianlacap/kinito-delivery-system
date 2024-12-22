using Backend.Database;
using Backend.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Admin.Users.Update;

public class Endpoint : Endpoint<UserUpdateReq>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Put("/admin/users/{id}");
        Roles(nameof(Role.Admin));
    }

    public override async Task HandleAsync(UserUpdateReq req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var isEmailTaken = await Db.Users.AnyAsync(x => x.Email == req.Email && x.Id != id, ct);

        if (isEmailTaken)
        {
            ThrowError(x => x.Email, "Email already taken by another user");
        }
        var user = await Db.Users.FirstOrDefaultAsync(x => x.Id == id, ct);
        req.Adapt(user);
        await Db.SaveChangesAsync(ct);
    }
}
