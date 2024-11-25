using MediaTRAndDapper.Common.ICommand;
using Platform.Api.Database.Repositories.Abstract;

namespace MediaTRAndDapper.CQRS.Commands.Product.AddProducts
{
    public class AddProductCommandHandler(IProductRepository productRepository) : ICommandHandler<AddProductCommand>
    {
        public async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(nameof(request));

            var product = new Models.Product
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Stock = request.Stock,
                CategoryId = request.CategoryId,

            };
            await productRepository.AddAsync(product);
            await Task.CompletedTask;
        }
    }
}
