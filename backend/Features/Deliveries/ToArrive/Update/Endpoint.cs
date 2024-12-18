using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Deliveries.ToArrive.Update;

public class Endpoint : EndpointWithoutRequest
{
    public AppDbContext Db { get; set; } = null!;
    public IUserService UserService { get; set; } = null!;

    public override void Configure()
    {
        Put("/deliveries/to-arrive/{referenceNumber}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var referenceNumber = Route<string>("referenceNumber");
        var delivery = await Db.Deliveries.FirstOrDefaultAsync(
            x => x.ReferenceNumber == referenceNumber,
            ct
        );
        if (delivery is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        var user = await Db.Users.FirstOrDefaultAsync(x => x.Id == UserService.UserId, ct);
        delivery.DeliveryStatus = DeliveryStatus.Shipped;
        var history = new DeliveryHistory
        {
            DeliveryId = delivery.Id,
            DeliveryStatus = DeliveryStatus.Shipped,
            Remarks =
                "Scanned by " + user!.LastName + ", " + user.FirstName + " " + user.MiddleName,
        };
        await Db.DeliveryHistories.AddAsync(history, ct);
        await Db.SaveChangesAsync(ct);
    }
}
