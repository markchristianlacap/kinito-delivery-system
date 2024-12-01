using Backend.Entities.Common;
using Backend.Enums;

namespace Backend.Entities;

public class DeliveryHistory : AuditEntity
{
    public Guid DeliveryId { get; set; }
    public Delivery Delivery { get; set; } = null!;
    public DeliveryStatus DeliveryStatus { get; set; }
    public string? Remarks { get; set; }
}
