using FastEndpoints;
using MediatR;

namespace MediaTRAndDapper.CQRS.Query.CustomerQuery.GetAllCustomers;

public class GetAllCustomersEndPoint(ISender sender) : Endpoint<GetAllCustomersRequest>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Get("/GetAllCustomers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllCustomersRequest req, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);
        var query = new GetAllCustomersQuery(req.Page, req.PageSize);

        var customers = await _sender.Send(query, ct);

        if (customers != null && customers.Any())
        {
            await SendAsync(customers, statusCode: StatusCodes.Status200OK, ct);
        }
        else
        {
            await SendAsync(new { Message = "No customers found" }, statusCode: StatusCodes.Status404NotFound, ct);
        }
    }
}
