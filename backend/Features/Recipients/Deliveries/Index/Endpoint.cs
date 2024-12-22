using Backend.Database;
using Mapster;

namespace Backend.Features.Recipients.Deliveries.Index;

public class Endpoint : Endpoint<RecipientDeliveryPagedReq, PagedRes<RecipientDeliveryRowRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/recipients/{id:guid}/deliveries");
    }

    public override async Task HandleAsync(RecipientDeliveryPagedReq req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var deliveries = await Db
            .Deliveries.ProjectToType<RecipientDeliveryRowRes>()
            .Where(x => x.RecipientId == id)
            .ToPagedAsync(req, ct);
        Response = deliveries;
    }
}
