using Backend.Entities;

namespace Backend.Database.Seeders;

public static class SizeTypesSeeder
{
    public static void Seed(AppDbContext context)
    {
        string[] sizes =
        [
            "Extra Small",
            "Small",
            "Medium",
            "# 1",
            "# 2",
            "# 3",
            "# 4",
            "# 5",
            "# 6",
            "# 7",
            "# 8",
            "# 9",
            "# 10",
            "# 11",
            "# 12",
            "# 13",
            "# 14",
            "# 15",
            "# 16",
            "# 17",
            "Long Box Oversize",
            "Oversize",
        ];

        foreach (string size in sizes)
        {
            if (!context.SizeTypes.Any(s => s.Name == size))
            {
                context.SizeTypes.Add(new SizeType { Name = size });
            }
        }
        context.SaveChanges();
    }
}
