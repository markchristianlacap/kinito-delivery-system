using Backend.Enums;

namespace Backend.Features.Deliveries;

public class DeliveryModel
{
    public DateTime Date { get; set; }
    public string TrackingNumber { get; set; } = null!;
    public string? ContactNumber { get; set; }
    public string Address { get; set; } = null!;
    public decimal Amount { get; set; }
    public PackageType PackageType { get; set; }
    public SizeType Size { get; set; }
    public Guid RecipientId { get; set; }
}