using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Options.PackageTypes;

public class Endpoint : EndpointWithoutRequest<List<PackageTypeOptionRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/options/package-types");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var query = Db.PackageTypes.AsQueryable();
        var res = await query
            .OrderBy(x => x.CreatedAt)
            .ProjectToType<PackageTypeOptionRes>()
            .ToListAsync(ct);
        Response = res;
    }
}
