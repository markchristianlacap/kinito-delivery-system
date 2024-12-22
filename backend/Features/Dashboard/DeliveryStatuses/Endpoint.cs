using Backend.Database;
using Backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Dashboard.DeliveryStatuses;

public class Endpoint : EndpointWithoutRequest<List<DeliveryStatusRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/dashboard/delivery-statuses");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var statuses = await Db
            .Deliveries.GroupBy(x => x.DeliveryStatus)
            .Select(x => new DeliveryStatusRes { DeliveryStatus = x.Key, Count = x.Count() })
            .ToListAsync(ct);
        var deliveryStatuses = Enum.GetValues<DeliveryStatus>();
        foreach (var status in deliveryStatuses)
        {
            if (!statuses.Any(x => x.DeliveryStatus == status))
            {
                statuses.Add(new DeliveryStatusRes { DeliveryStatus = status, Count = 0 });
            }
        }
        Response = statuses;
    }
}
