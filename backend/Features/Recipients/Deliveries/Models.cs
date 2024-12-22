using Backend.Enums;

namespace Backend.Features.Recipients.Deliveries;

public class RecipientDeliveryModel
{
    public Guid Id { get; set; }
    public string ReferenceNumber { get; set; } = null!;
    public DateTime Date { get; set; }
    public string TrackingNumber { get; set; } = null!;
    public string? ContactNumber { get; set; }
    public string Address { get; set; } = null!;
    public decimal Amount { get; set; }
    public Guid PackageTypeId { get; set; }
    public Guid SizeTypeId { get; set; }
    public Guid RecipientId { get; set; }
    public string RecipientName { get; set; } = null!;
    public string SizeTypeName { get; set; } = null!;
    public string PackageTypeName { get; set; } = null!;
    public DeliveryStatus DeliveryStatus { get; set; }
}
