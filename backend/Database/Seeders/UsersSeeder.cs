using Backend.Entities;
using Backend.Enums;

namespace Backend.Database.Seeders;

public static class UsersSeeder
{
    public static void Seed(AppDbContext context)
    {
        var admin = new User
        {
            LastName = "Admin",
            FirstName = "Kinito",
            Email = "admin@admin.com",
            Password = BCrypt.Net.BCrypt.EnhancedHashPassword("password"),
            ContactNumber = "n/a",
            Address = "n/a",
            Role = Role.Admin,
        };

        if (!context.Users.Any(u => u.Email == admin.Email))
        {
            context.Users.Add(admin);
        }
        context.SaveChanges();
    }
}
