using FastEndpoints;
using MediatR;
using MediaTRAndDapper.CQRS.Query.CustomerQuery.GetCustomer;

namespace MediaTRAndDapper.CQRS.Query.CustomerQuery;

public class GetCustomerEndPoint(ISender sender) : Endpoint<GetCustomerRequest>
{
    private readonly ISender _sender = sender;
    public override void Configure()
    {
        Get("/GetCustomer");
        AllowAnonymous();
    }
    public override async Task HandleAsync(GetCustomerRequest req, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);
        var query = new GetCustomerQuery(req.Id);
        var customer = await _sender.Send(query, ct);
        if (customer != null)
        {
            await SendAsync(customer, statusCode: StatusCodes.Status200OK, ct);
        }
        else
        {
            await SendAsync(new { Message = "Customer not found" }, statusCode: StatusCodes.Status404NotFound, ct);
        }
    }
}
