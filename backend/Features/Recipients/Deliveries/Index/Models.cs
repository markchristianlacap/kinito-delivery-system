using Backend.Database;
using Humanizer;

namespace Backend.Features.Recipients.Deliveries.Index;

public class RecipientDeliveryRowRes : RecipientDeliveryModel
{
    public string DeliveryStatusDesc => DeliveryStatus.Humanize(LetterCasing.Title);
}

public class RecipientDeliveryPagedReq : PagedReq { }
