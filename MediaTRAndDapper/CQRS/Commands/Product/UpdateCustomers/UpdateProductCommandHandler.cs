using MediatR;
using Platform.Api.Database.Repositories.Abstract;

namespace MediaTRAndDapper.CQRS.Commands.Product.UpdateCustomers
{
    public class UpdateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
    {

        public async Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return new UpdateProductResponse(false, "Product not found");
            }
            
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.CategoryId = product.CategoryId;

            await productRepository.UpdateAsync(product);

            return new UpdateProductResponse(true, "Product updated successfully");
        }
    }
}
