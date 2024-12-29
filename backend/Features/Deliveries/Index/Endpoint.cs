using Backend.Database;
using Backend.Entities;
using Mapster;

namespace Backend.Features.Deliveries.Index;

public class Endpoint : Endpoint<DeliveryPagedReq, PagedRes<DeliveryRowRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/deliveries");
    }

    public override async Task HandleAsync(DeliveryPagedReq req, CancellationToken ct)
    {
        var query = Db.Deliveries.AsQueryable();
        if (req.Search is not null)
        {
            query = query.Where(d =>
                d.Recipient.FirstName.Contains(req.Search)
                || d.Recipient.MiddleName!.Contains(req.Search)
                || d.Recipient.LastName.Contains(req.Search)
                || d.Address.Contains(req.Search)
                || d.ReferenceNumber.Contains(req.Search)
                || d.TrackingNumber.Contains(req.Search)
            );
        }
        var cfg = new TypeAdapterConfig();
        cfg.NewConfig<Delivery, DeliveryRowRes>()
            .Map(
                d => d.RecipientName,
                s =>
                    s.Recipient.LastName
                    + ", "
                    + s.Recipient.FirstName
                    + " "
                    + s.Recipient.MiddleName
            );

        var res = await query.ProjectToType<DeliveryRowRes>(cfg).ToPagedAsync(req, ct);
        Response = res;
    }
}
