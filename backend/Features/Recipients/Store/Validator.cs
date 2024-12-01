using FluentValidation;

namespace Backend.Features.Recipients.Store;

public class Validator : Validator<RecipientStoreReq>
{
    public Validator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.ContactNumber).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
    }
}
