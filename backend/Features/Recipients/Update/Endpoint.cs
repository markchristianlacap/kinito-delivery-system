using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Recipients.Update;

public class Endpoint : Endpoint<RecipientUpdateReq, RecipientUpdateRes>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Put("/recipients/{id:guid}");
    }

    public override async Task HandleAsync(RecipientUpdateReq req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var recipient = await Db.Recipients.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (recipient is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        req.Adapt(recipient);
        await Db.SaveChangesAsync(ct);
        Response = recipient.Adapt<RecipientUpdateRes>();
    }
}
