using Backend.Enums;
using Humanizer;

namespace Backend.Features.Deliveries.Show;

public class DeliveryShowRes : DeliveryModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PackageTypeDesc => PackageType.Humanize(LetterCasing.Title);
    public string SizeDesc => Size.Humanize(LetterCasing.Title);
    public string RecipientName { get; set; } = null!;
    public string ReferenceNumber { get; set; } = null!;
    public DeliveryStatus DeliveryStatus { get; set; }
    public string DeliveryStatusDesc => DeliveryStatus.Humanize(LetterCasing.Title);
}
