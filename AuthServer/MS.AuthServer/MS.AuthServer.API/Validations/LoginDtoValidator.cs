using FluentValidation;
using MS.AuthServer.Core.DTOs;

namespace MS.AuthServer.API.Validations
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {

        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Email is wrong");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
