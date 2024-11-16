using FastEndpoints;
using MediatR;
using MediaTRAndDapper.CQRS.Commands.Customer.UpdateCustomers;

namespace MediaTRAndDapper.CQRS.Commands.Customer.EndPoints;

public class UpdateCustomerEndPoint(ISender sender) : Endpoint<UpdateCustomerRequest>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Put("/UpdateCustomer");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateCustomerRequest req, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        var command = new UpdateCustomerCommand(
            req.Id,
            req.FullName,
            req.Email,
            req.PhoneNumber,
            req.PasswordHash,      
            req.PasswordSalt,          
            req.LastPasswordChange,      
            req.Address,
            req.Active ?? false,
            req.Deleted ?? false,
            req.RefreshToken ?? string.Empty,
            req.RefreshTokenExpiry,  
            req.CreatedAt ?? DateTime.Now,
            req.NewPassword,
            req.OrdersId           
        );

        var response = _sender.Send(command, ct);

        if (response != null)
        {
            await SendAsync(new { Message = "Customer updated successfully" }, statusCode: StatusCodes.Status200OK, ct);
        }
        else
        {
            await SendAsync(new { Message = "Failed to update customer" }, statusCode: StatusCodes.Status400BadRequest, ct);
        }
    }
}
