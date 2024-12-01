using Backend.Database;
using Backend.Entities;
using Mapster;

namespace Backend.Features.Recipients.Store;

public class Endpoint : Endpoint<RecipientStoreReq, RecipientStoreRes>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Post("/recipients");
    }

    public override async Task HandleAsync(RecipientStoreReq req, CancellationToken ct)
    {
        var recipient = req.Adapt<Recipient>();
        await Db.Recipients.AddAsync(recipient, ct);
        await Db.SaveChangesAsync(ct);
        Response = recipient.Adapt<RecipientStoreRes>();
    }
}
