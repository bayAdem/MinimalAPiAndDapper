using MediatR;
using Platform.Api.Database.Repositories.Abstract;

namespace MediaTRAndDapper.CQRS.Commands.Category.UpdateCategories
{
    public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IProductRepository productRepository) : IRequestHandler<UpdateCategoryRequest, UpdateCategoryResponse>
    {

        public async Task<UpdateCategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetByIdAsync(request.Id);

            if (category == null)
            {
                return new UpdateCategoryResponse(false, "Category not found");
            }

            var products = await productRepository.GetProductsByIdsAsync(request.ProductIds);

            if (products == null || products.Count == 0)
            {
                return new UpdateCategoryResponse(false, "Products not found");
            }

            category.Name = request.Name;
            category.Description = request.Description;
            category.CreatedAt = request.CreatedAt ?? category.CreatedAt;
            category.Products = products;

            await categoryRepository.UpdateAsync(category);

            return new UpdateCategoryResponse(true, "Category updated successfully");
        }
    }



}
