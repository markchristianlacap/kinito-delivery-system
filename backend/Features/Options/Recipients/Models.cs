namespace Backend.Features.Options.Recipients;

public class RecipientOptionRes
{
    public Guid Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string FullName
    {
        get { return $"{LastName}, {FirstName} {MiddleName}"; }
    }
    public string Address { get; set; } = null!;
    public string ContactNumber { get; set; } = null!;
    public string? Email { get; set; }
    public int TotalDeliveries { get; set; }
}

public class RecipientOptionReq
{
    public string? Search { get; set; }
    public Guid? Id { get; set; }
}
