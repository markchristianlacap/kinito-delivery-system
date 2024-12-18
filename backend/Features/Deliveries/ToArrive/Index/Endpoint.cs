using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Mapster;

namespace Backend.Features.Deliveries.ToArrive.Index;

public class Endpoint : Endpoint<DeliveryPagedReq, PagedRes<DeliveryRowRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/deliveries/to-arrive");
    }

    public override async Task HandleAsync(DeliveryPagedReq req, CancellationToken ct)
    {
        var query = Db
            .Deliveries.AsQueryable()
            .Where(x =>
                x.DeliveryStatus == DeliveryStatus.Arrive
                || x.DeliveryStatus == DeliveryStatus.Shipped
            );
        if (req.ArrivalDate is not null)
        {
            query = query.Where(x => x.Date.Date == req.ArrivalDate.Value.Date);
        }
        if (req.IsArrived is not null)
        {
            var status = req.IsArrived.Value ? DeliveryStatus.Arrive : DeliveryStatus.Shipped;
            query = query.Where(x => x.DeliveryStatus == status);
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
