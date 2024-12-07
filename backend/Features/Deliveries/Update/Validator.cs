using FluentValidation;

namespace Backend.Features.Deliveries.Update;

public class Validator : Validator<DeliveryUpdateReq>
{
    public Validator()
    {
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.TrackingNumber).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.PackageTypeId).NotEmpty();
        RuleFor(x => x.SizeTypeId).NotEmpty();
        RuleFor(x => x.RecipientId).NotEmpty();
    }
}
