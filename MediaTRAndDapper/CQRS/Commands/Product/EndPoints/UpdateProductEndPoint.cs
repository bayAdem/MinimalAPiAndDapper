using FastEndpoints;
using MediatR;
using MediaTRAndDapper.CQRS.Commands.Product.UpdateCustomers;

namespace MediaTRAndDapper.CQRS.Commands.Product.EndPoints
{
    public class UpdateProductEndPoint(ISender sender) : Endpoint<UpdateProductRequest>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Put("/UpdateProduct");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(req);

            var command = new UpdateProductCommand(
                req.Id,
                req.Name,
                req.Description,
                req.Price,
                req.Stock,
                req.CategoryId
                );

            var response = _sender.Send(command);
            if (response != null)
            {
                await SendAsync(new { Message = "Product Update successfully" }, statusCode: StatusCodes.Status200OK, ct);
            }
            await SendAsync(new { Message = "Product Update successfully" }, statusCode: StatusCodes.Status400BadRequest, ct);
        }
    }
}
