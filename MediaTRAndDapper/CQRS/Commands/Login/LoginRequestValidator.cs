
using FastEndpoints;
using FluentValidation;

namespace MediaTRAndDapper.CQRS.Commands.Login;

public sealed class LoginRequestValidator : Validator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.Email)
            .NotNull().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(request => request.Password)
            .NotNull().WithMessage("Password is required.");
    }
}
