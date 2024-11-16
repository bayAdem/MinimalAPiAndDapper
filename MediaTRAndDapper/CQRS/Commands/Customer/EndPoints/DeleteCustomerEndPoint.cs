using FastEndpoints;
using MediatR;
using MediaTRAndDapper.CQRS.Commands.Customer.DeleteCustomers;

namespace MediaTRAndDapper.CQRS.Commands.Customer.EndPoints
{
    public class DeleteCustomerEndPoint(ISender sender) : Endpoint<DeleteCustomerRequest>
    {
        private readonly ISender _sender = sender;

        public override void Configure()
        {
            Delete("/DeleteCustomer");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteCustomerRequest req, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(req);
            var command = new DeleteCustomerCommand(req.id);
            var response = _sender.Send(command, ct);
            if (response != null)
            {
                await SendAsync(new { Message = "Customer deleted successfully" }, statusCode: StatusCodes.Status200OK, ct);
            }
            else
            {
                await SendAsync(new { Message = "Failed to delete customer" }, statusCode: StatusCodes.Status400BadRequest, ct);
            }
        }
    }

}
