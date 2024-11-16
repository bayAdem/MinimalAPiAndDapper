using FastEndpoints;
using MediatR;
using MediaTRAndDapper.CQRS.Commands.Product.AddProducts;

namespace MediaTRAndDapper.CQRS.Commands.Product.EndPoints
{
    public class ProductEndPoint(ISender sender) : Endpoint<AddProductRequest>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Post("/AddProduct");
            AllowAnonymous();
        }

        public override async Task HandleAsync(AddProductRequest req, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(req);

            var command = new AddProductCommand(
                req.ProductId,
                req.Name,
                req.Description,
                req.Price,
                req.Stock,
                req.CategoryIds);
            await _sender.Send(command, ct);
            await SendAsync(req.ProductId, statusCode: StatusCodes.Status201Created, ct);
        }
    }
}
