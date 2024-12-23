﻿using FluentValidation;

namespace Backend.Features.Admin.Users.Update;

public class Validator : Validator<UserUpdateReq>
{
    public Validator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.ContactNumber).NotEmpty();
        RuleFor(x => x.Birthdate).NotNull();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Role).NotNull().IsInEnum();
    }
}
