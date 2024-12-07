using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Backend.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Deliveries.Store;

public class Endpoint : Endpoint<DeliveryStoreReq>
{
    public AppDbContext Db { get; set; } = null!;
    public IReferenceNumberService ReferenceNumberService { get; set; } = null!;
    public IUserService UserService { get; set; } = null!;

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
        var existPackageType = await Db.PackageTypes.AnyAsync(x => x.Id == req.PackageTypeId, ct);
        if (existPackageType is false)
        {
            ThrowError(x => x.PackageTypeId, "Package type not found");
            return;
        }
        var existSize = await Db.SizeTypes.AnyAsync(x => x.Id == req.SizeTypeId, ct);
        if (existSize is false)
        {
            ThrowError(x => x.SizeTypeId, "Size not found");
            return;
        }
        var currentUser = await Db.Users.FirstOrDefaultAsync(x => x.Id == UserService.UserId, ct);
        if (currentUser is null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        var delivery = req.Adapt<Delivery>();
        delivery.ReferenceNumber = await ReferenceNumberService.GenerateReferenceNumberAsync(ct);
        delivery.DeliveryStatus = DeliveryStatus.Encoded;
        var history = new DeliveryHistory
        {
            DeliveryStatus = DeliveryStatus.Encoded,
            Remarks =
                "Delivery encoded by "
                + currentUser.LastName
                + " "
                + currentUser.FirstName
                + " with reference number "
                + delivery.ReferenceNumber,
        };
        delivery.Histories.Add(history);
        await Db.Deliveries.AddAsync(delivery, ct);
        await Db.SaveChangesAsync(ct);
    }
}
