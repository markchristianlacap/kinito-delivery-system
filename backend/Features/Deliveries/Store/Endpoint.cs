using Backend.Database;
using Backend.Entities;
using Backend.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Deliveries.Store;

public class Endpoint : Endpoint<DeliveryStoreReq>
{
    public AppDbContext Db { get; set; } = null!;
    public IReferenceNumberService ReferenceNumberService { get; set; } = null!;

    public override void Configure()
    {
        Post("/deliveries");
    }

    public override async Task HandleAsync(DeliveryStoreReq req, CancellationToken ct)
    {
        var existRecipient = await Db.Recipients.AnyAsync(x => x.Id == req.RecipientId, ct);
        if (existRecipient is false)
        {
            ThrowError(x => x.RecipientId, "Recipient not found");
            return;
        }
        var delivery = req.Adapt<Delivery>();
        delivery.ReferenceNumber = await ReferenceNumberService.GenerateReferenceNumberAsync(ct);
        await Db.Deliveries.AddAsync(delivery, ct);
        await Db.SaveChangesAsync(ct);
    }
}
