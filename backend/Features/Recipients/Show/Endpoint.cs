using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Recipients.Show;

public class Endpoint : EndpointWithoutRequest<RecipientShowRes>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/recipients/{id:guid}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var recipient = await Db
            .Recipients.ProjectToType<RecipientShowRes>()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        if (recipient is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        Response = recipient;
    }
}
