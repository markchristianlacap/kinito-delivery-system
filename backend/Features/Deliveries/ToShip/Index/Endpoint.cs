using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Deliveries.ToShip.Index;

public class Endpoint : Endpoint<DeliveryPagedReq, ToShipDeliveryPagedRes>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/deliveries/to-ship");
    }

    public override async Task HandleAsync(DeliveryPagedReq req, CancellationToken ct)
    {
        var query = Db
            .Deliveries.AsQueryable()
            .Where(x =>
                (
                    x.DeliveryStatus == DeliveryStatus.Encoded
                    || x.DeliveryStatus == DeliveryStatus.Shipped
                )
                && x.Date.Date == req.ArrivalDate.Date
            );
        if (req.IsShipped is not null)
        {
            var status = req.IsShipped.Value ? DeliveryStatus.Shipped : DeliveryStatus.Encoded;
            query = query.Where(x => x.DeliveryStatus == status);
        }
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

        var paged = await query.ProjectToType<DeliveryRowRes>(cfg).ToPagedAsync(req, ct);
        var res = paged.Adapt<ToShipDeliveryPagedRes>();
        res.TotalShipped = await Db
            .Deliveries.Where(x =>
                x.Date.Date == req.ArrivalDate.Date && x.DeliveryStatus == DeliveryStatus.Shipped
            )
            .CountAsync(ct);
        Response = res;
    }
}
