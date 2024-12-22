using FluentValidation;
using MS.AuthServer.Core.DTOs;

namespace MS.AuthServer.API.Validations;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is Required").EmailAddress().WithMessage("Email is wrong");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");

        RuleFor(x => x.BirthDate).NotEmpty().WithMessage("BirthDate is required");
        RuleFor(x => x.TCKN).NotEmpty().WithMessage("TCKN is required").Length(11).WithMessage("TCKN must be 11 characters");
    }
}

