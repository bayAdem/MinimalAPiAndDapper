using MediatR;
using Platform.Api.Database.Repositories.Abstract;

namespace MediaTRAndDapper.CQRS.Commands.Category.AddCategories;

//Ekleme işlemi.
public class AddCategoryCommandHandler(ICategoryRepository categoryRepository, IProductRepository productRepository) : IRequestHandler<AddCategoryCommand, AddCategoryResponse<Models.Category>>
{

    public async Task<AddCategoryResponse<Models.Category>> Handle(AddCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = new Models.Category
        {
            Name = command.Name,
            Description = command.Description,
            CreatedAt = command.CreatedAt
        };

        await categoryRepository.AddAsync(category);

        if (command.ProductId != null && command.ProductId.Count != 0)
        {
            var products = await productRepository.GetProductsByIdsAsync(command.ProductId);

            foreach (var product in products)
            {
                product.CategoryId = category.Id;
                await productRepository.UpdateAsync(product);
            }
        }

        return new AddCategoryResponse<Models.Category>(true, "Category updated successfully", category);
    }
}
