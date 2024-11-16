using FastEndpoints;
using MediatR;
using MediaTRAndDapper.CQRS.Query.CustomerQuery.GetCustomer;

namespace MediaTRAndDapper.CQRS.Query.CustomerQuery;

public class GetCustomerOrdersEndPoint(ISender sender) : Endpoint<GetCustomerOrdersRequest>
{
    private readonly ISender _sender = sender;
    public override void Configure()
    {
        Get("/GetCustomerOrders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetCustomerOrdersRequest req, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        var query = new GetCustomerOrdersQuery(req.CustomerId);
        var orders = await _sender.Send(query, ct);

        if (orders != null && orders.Any())
        {
            await SendAsync(orders, statusCode: StatusCodes.Status200OK, ct);
        }
        else
        {
            await SendAsync(new { Message = "No orders found for the customer" }, statusCode: StatusCodes.Status404NotFound, ct);
        }
    }
}
