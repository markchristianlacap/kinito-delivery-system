using Backend.Entities;

namespace Backend.Database.Seeders;

public static class PackageTypesSeeder
{
    public static void Seed(AppDbContext context)
    {
        string[] packageTypes =
        [
            "Pouch",
            "Box",
            "Flat Box",
            "Long Box",
            "Long Box Oversize",
            "Oversize",
        ];

        foreach (string packageType in packageTypes)
        {
            if (!context.PackageTypes.Any(s => s.Name == packageType))
            {
                context.PackageTypes.Add(new PackageType { Name = packageType });
            }
        }
        context.SaveChanges();
    }
}
