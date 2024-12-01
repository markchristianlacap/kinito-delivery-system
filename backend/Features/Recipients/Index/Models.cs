using Backend.Database;

namespace Backend.Features.Recipients.Index;

public class RecipientRowRes : RecipientModel
{
    public Guid Id { get; set; }
}

public class RecipientPagedReq : PagedReq
{
    public string? Search { get; set; }
}
