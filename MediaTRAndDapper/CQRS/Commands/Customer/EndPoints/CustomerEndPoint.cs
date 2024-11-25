using FastEndpoints;
using MediatR;
using MediaTRAndDapper.CQRS.Commands.Customer.AddCustomers;
using MediaTRAndDapper.CQRS.Commands.Customer.UpdateCustomers;

namespace MediaTRAndDapper.CQRS.Commands.Customer.EndPoints
{
    public class CustomerEndPoint : Endpoint<AddCustomerRequest>
    {
        private readonly ISender _sender;

        public CustomerEndPoint(ISender sender) => _sender = sender;

        public override void Configure()
        {
            Post("/AddCustomer");
            AllowAnonymous();
        }

        public override async Task HandleAsync(AddCustomerRequest req, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(req);

            var (passwordHash, passwordSalt) = PasswordHelper.HashPassword(req.Password);

            var command = new AddCustomerCommand(
                req.FullName,
                req.Email,
                req.PhoneNumber,
                passwordHash,
                passwordSalt,
                req.LastPasswordChange,
                req.Address,
                req.Active,
                req.Deleted,
                req.RefreshToken,
                req.RefreshTokenExpiry ?? DateTime.UtcNow,
                req.CreatedAt ?? DateTime.UtcNow
            );

            await _sender.Send(command, ct);
            await SendAsync(new { Message = "Customer created successfully" }, StatusCodes.Status201Created, ct);
        }

    }
}
