using Exam_CA.Application.DTOs;
using FluentValidation;

namespace Exam_CA.WebApi.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(l => l.login).NotNull();
            RuleFor(l => l.password).NotNull();
            RuleFor(l => l.login).EmailAddress().WithMessage("El login debe ser con el formato de email");

        }
    }
}
