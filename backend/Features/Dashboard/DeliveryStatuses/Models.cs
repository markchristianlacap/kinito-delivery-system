using Backend.Enums;
using Humanizer;

namespace Backend.Features.Dashboard.DeliveryStatuses;

public class DeliveryStatusRes
{
    public DeliveryStatus DeliveryStatus { get; set; }
    public string DeliveryStatusDesc => DeliveryStatus.Humanize(LetterCasing.Title);
    public int Count { get; set; }
}
