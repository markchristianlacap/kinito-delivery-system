using Backend.Database;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Deliveries.Update;

public class Endpoint : Endpoint<DeliveryUpdateReq>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Put("/deliveries/{id:guid}");
    }

    public override async Task HandleAsync(DeliveryUpdateReq req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var existRecipient = await Db.Recipients.AnyAsync(x => x.Id == req.RecipientId, ct);
        if (existRecipient is false)
        {
            ThrowError(x => x.RecipientId, "Recipient not found");
            return;
        }
        var delivery = await Db.Deliveries.FirstOrDefaultAsync(x => x.Id == id, ct);
    }
}
