using FluentValidation;

namespace Backend.Features.Deliveries.Store;

public class Validator : Validator<DeliveryStoreReq>
{
    public Validator()
    {
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.TrackingNumber).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.PackageType).IsInEnum();
        RuleFor(x => x.Size).IsInEnum();
        RuleFor(x => x.RecipientId).NotEmpty();
    }
}
