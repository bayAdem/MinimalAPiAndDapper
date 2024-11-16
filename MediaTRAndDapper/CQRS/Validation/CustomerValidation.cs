using FluentValidation;

namespace MediaTRAndDapper.CQRS.Validation;

public class CustomerValidator : AbstractValidator<Models.Customer>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.FullName)
            .Length(1, 100).WithMessage("FullName cannot be longer than 100 characters.");

        RuleFor(customer => customer.Email)
            .EmailAddress().WithMessage("Invalid email address.")
            .Length(1, 200).WithMessage("Email cannot be longer than 200 characters.");

        RuleFor(customer => customer.PhoneNumber)
            .NotEmpty().WithMessage("PhoneNumber is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number.");

        RuleFor(customer => customer.Address)
            .NotEmpty().WithMessage("Address is required.")
            .Length(1, 500).WithMessage("Address cannot be longer than 500 characters.");

        RuleFor(customer => customer.LastPasswordChange)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("LastPasswordChange cannot be a future date.");

        RuleFor(customer => customer.Active)
            .NotNull().WithMessage("Active status is required.");

        RuleFor(customer => customer.Deleted)
            .NotNull().WithMessage("Deleted status is required.");

        RuleFor(customer => customer.CreatedDate)
            .NotEmpty().WithMessage("CreatedDate is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("CreatedDate cannot be a future date.");

        RuleFor(customer => customer.RefreshToken)
            .MaximumLength(200).WithMessage("RefreshToken cannot be longer than 200 characters.");

        RuleFor(customer => customer.RefreshTokenExpiry)
            .GreaterThanOrEqualTo(DateTime.Now).When(c => c.RefreshToken != null)
            .WithMessage("RefreshTokenExpiry must be in the future.");

        RuleFor(customer => customer.Orders)
            .NotEmpty().WithMessage("Orders are required.")
            .Must(orders => orders.Count > 0).WithMessage("Orders cannot be empty.");
    }
}
