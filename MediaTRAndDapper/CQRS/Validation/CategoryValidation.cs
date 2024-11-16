using FluentValidation;
using MediaTRAndDapper.CQRS.Commands.Category.AddCategories;

namespace MediaTRAndDapper.CQRS.Validation
{
    public class CategoryValidation : AbstractValidator<AddCategoryRequest>
    {
        public CategoryValidation()
        {
            // Name alanının boş olmaması gerektiğini kontrol et
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .Length(3, 100).WithMessage("Name must be between 3 and 100 characters");

            // Description alanının boş olmaması gerektiğini kontrol et
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty")
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters");

            // CreatedAt tarihinin mevcut olmasını kontrol et (isteğe bağlı)
            RuleFor(x => x.CreatedAt)
                .NotNull().WithMessage("CreatedAt must have a value")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("CreatedAt cannot be in the future");

            // Products koleksiyonunun boş olmaması gerektiğini kontrol et
            RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("At least one product ID must be provided");
        }
    }
}
