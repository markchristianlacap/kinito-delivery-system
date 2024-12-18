using Backend.Database;
using Backend.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Options.Recipients;

public class Endpoint : Endpoint<RecipientOptionReq, List<RecipientOptionRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/options/recipients");
    }

    public override async Task HandleAsync(RecipientOptionReq req, CancellationToken ct)
    {
        var query = Db.Recipients.AsQueryable();
        if (req.Search is not null)
        {
            query = query.Where(x =>
                (x.LastName + " ," + x.FirstName + " " + x.MiddleName).Contains(req.Search)
                || x.Id == req.Id
            );
        }
        var mapCfg = new TypeAdapterConfig();
        mapCfg
            .NewConfig<Recipient, RecipientOptionRes>()
            .Map(dest => dest.TotalDeliveries, src => src.Deliveries.Count);
        var res = await query
            .ProjectToType<RecipientOptionRes>(mapCfg)
            .OrderBy(x => req.Id == x.Id ? 0 : 1)
            .ToListAsync(ct);
        Response = res;
    }
}
