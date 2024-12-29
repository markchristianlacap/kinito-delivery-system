using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;

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
                (
                    x.DeliveryStatus == DeliveryStatus.Arrive
                    || x.DeliveryStatus == DeliveryStatus.Shipped
                )
                && x.Date.Date == req.ArrivalDate.Date
            );
        if (req.IsArrived is not null)
        {
            var status = req.IsArrived.Value ? DeliveryStatus.Arrive : DeliveryStatus.Shipped;
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
        var res = paged.Adapt<ToArriveDeliveryPagedRes>();
        res.TotalArrived = await Db.Deliveries.CountAsync(
            x => x.DeliveryStatus == DeliveryStatus.Arrive && x.Date.Date == req.ArrivalDate.Date,
            ct
        );
        Response = res;
    }
}
