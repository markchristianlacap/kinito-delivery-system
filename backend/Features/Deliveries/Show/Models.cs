using Backend.Enums;
using Humanizer;

namespace Backend.Features.Deliveries.Show;

public class DeliveryShowRes : DeliveryModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PackageTypeName { get; set; } = null!;
    public string SizeTypeName { get; set; } = null!;
    public string RecipientName { get; set; } = null!;
    public string ReferenceNumber { get; set; } = null!;
    public DeliveryStatus DeliveryStatus { get; set; }
    public string DeliveryStatusDesc => DeliveryStatus.Humanize(LetterCasing.Title);
}
