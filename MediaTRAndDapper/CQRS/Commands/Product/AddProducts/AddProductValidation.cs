namespace MediaTRAndDapper.CQRS.Commands.Product.AddProducts;

using FluentValidation;
using System.Text.RegularExpressions;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MinimumLength(10).WithMessage("Description must be at least 10 characters long.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.")
            .Must(HaveValidDecimalPlaces);

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be a positive integer.");
    }

    private bool HaveValidDecimalPlaces(decimal price)
    {
        var regex = new Regex(@"^\d{1,16}(\.\d{1,2})?$");
        return regex.IsMatch(price.ToString());
    }
}
