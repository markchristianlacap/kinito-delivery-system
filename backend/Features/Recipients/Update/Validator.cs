using FluentValidation;

namespace Backend.Features.Recipients.Update;

public class Validator : Validator<RecipientUpdateReq>
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
