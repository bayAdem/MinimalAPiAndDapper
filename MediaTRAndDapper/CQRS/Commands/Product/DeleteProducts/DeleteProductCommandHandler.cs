using MediaTRAndDapper.Common.ICommand;
using Platform.Api.Database.Repositories.Abstract;

namespace MediaTRAndDapper.CQRS.Commands.Product.DeleteProducts
{
    public class DeleteProductCommandHandler(IProductRepository productRepository) : ICommandHandler<DeleteProductCommand>
    {
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new ArgumentException("Id Bulunamadı");
            }
            await productRepository.DeleteAsync(request.Id);
        }
    }
}
