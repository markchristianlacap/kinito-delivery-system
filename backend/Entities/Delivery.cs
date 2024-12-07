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
    public Guid PackageTypeId { get; set; }
    public Guid SizeTypeId { get; set; }
    public Guid RecipientId { get; set; }
    public Recipient Recipient { get; set; } = null!;
    public SizeType SizeType { get; set; } = null!;
    public PackageType PackageType { get; set; } = null!;
    public DeliveryStatus DeliveryStatus { get; set; }
}
