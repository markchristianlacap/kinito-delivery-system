﻿using Backend.Database;
using Backend.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;
using UserEntity = Backend.Entities.User;

namespace Backend.Features.Admin.Users.Store;

public class Endpoint : Endpoint<UserStoreReq>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Post("/admin/users");
        Roles(nameof(Role.Admin));
    }

    public override async Task HandleAsync(UserStoreReq req, CancellationToken ct)
    {
        var isEmailTaken = await Db.Users.AnyAsync(x => x.Email == req.Email, ct);

        if (isEmailTaken)
        {
            ThrowError(x => x.Email, "Email already taken by another user");
        }
        var user = req.Adapt<UserEntity>();
        user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(req.Password);
        await Db.Users.AddAsync(user, ct);
        await Db.SaveChangesAsync(ct);
    }
}
