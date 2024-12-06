namespace Backend.Database.Seeders;

public static class Seeder
{
    public static void Seed(AppDbContext context)
    {
        UsersSeeder.Seed(context);
        PackageTypesSeeder.Seed(context);
        SizeTypesSeeder.Seed(context);
    }
}
