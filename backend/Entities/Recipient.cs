using Backend.Entities.Common;

namespace Backend.Entities;

public class Recipient : AuditEntity
{
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string Address { get; set; } = null!;
    public string ContactNumber { get; set; } = null!;
    public string? Email { get; set; }
}
