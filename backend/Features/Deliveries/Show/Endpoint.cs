using Backend.Database;
using Backend.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Deliveries.Show;

public class Endpoint : EndpointWithoutRequest<DeliveryShowRes>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/deliveries/{id:guid}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var cfg = new TypeAdapterConfig();
        cfg.NewConfig<Delivery, DeliveryShowRes>()
            .Map(
                d => d.RecipientName,
                s =>
                    s.Recipient.LastName
                    + ", "
                    + s.Recipient.FirstName
                    + " "
                    + s.Recipient.MiddleName
            );

        var res = await Db
            .Deliveries.ProjectToType<DeliveryShowRes>(cfg)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id, ct);
        if (res is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        Response = res;
    }
}
