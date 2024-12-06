using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Options.SizeTypes;

public class Endpoint : EndpointWithoutRequest<List<SizeTypeOptionRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/options/size-types");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var query = Db.SizeTypes.AsQueryable();

        var res = await query
            .OrderBy(x => x.CreatedAt)
            .ProjectToType<SizeTypeOptionRes>()
            .ToListAsync(ct);
        Response = res;
    }
}
