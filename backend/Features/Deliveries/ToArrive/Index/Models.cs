using Backend.Database;
using Backend.Enums;
using Humanizer;

namespace Backend.Features.Deliveries.ToArrive.Index;

public class DeliveryRowRes : DeliveryModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PackageTypeName { get; set; } = null!;
    public string SizeTypeName { get; set; } = null!;
    public string RecipientName { get; set; } = null!;
    public string ReferenceNumber { get; set; } = null!;
    public DeliveryStatus DeliveryStatus { get; set; }
    public string DeliveryStatusDesc => DeliveryStatus.Humanize(LetterCasing.Title);
    public bool IsArrived => DeliveryStatus == DeliveryStatus.Arrive;
}

public class ToArriveDeliveryPagedRes : PagedRes<DeliveryRowRes>
{
    public int TotalArrived { get; set; }
}

public class DeliveryPagedReq : PagedReq
{
    public string? Search { get; set; } = null!;
    public DateTime ArrivalDate { get; set; }
    public bool? IsArrived { get; set; }
}
