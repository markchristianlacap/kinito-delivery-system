using Backend.Database;
using Mapster;

namespace Backend.Features.Recipients.Index;

public class Endpoint : Endpoint<RecipientPagedReq, PagedRes<RecipientRowRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/recipients");
    }

    public override async Task HandleAsync(RecipientPagedReq req, CancellationToken ct)
    {
        var query = Db.Recipients.AsQueryable();
        if (req.Search is not null)
        {
            query = query.Where(x =>
                x.FirstName.Contains(req.Search) || x.LastName.Contains(req.Search)
            );
        }
        var res = await query.ProjectToType<RecipientRowRes>().ToPagedAsync(req, ct);
        Response = res;
    }
}
