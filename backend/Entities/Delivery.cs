using Backend.Entities.Common;
using Backend.Enums;

namespace Backend.Entities;

public class Delivery : AuditEntity
{
    public string ReferenceNumber { get; set; } = null!;
    public DateTime Date { get; set; }
    public string TrackingNumber { get; set; } = null!;
    public string? ContactNumber { get; set; }
    public string Address { get; set; } = null!;
    public decimal Amount { get; set; }
    public PackageType PackageType { get; set; }
    public SizeType Size { get; set; }
    public Guid RecipientId { get; set; }
    public Recipient Recipient { get; set; } = null!;
    public DeliveryStatus DeliveryStatus { get; set; }
}
