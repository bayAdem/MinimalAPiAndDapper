using FastEndpoints;
using MediatR;
using MediaTRAndDapper.Services;

namespace MediaTRAndDapper.CQRS.Commands.Login;

public class LoginEndPoint(IAuthenticationService authenticationService, ISender sender) : Endpoint<LoginRequest>
{

    public override void Configure()
    {
        Post("api/login");
        Description(b => b.Accepts<LoginRequest>("application/json")
        .Produces<LoginResponse>(200, "application/json")
        .ProducesProblemFE(400)
        .ProducesProblemFE<InternalErrorResponse>(500),
        clearDefaults: true);
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        GetCustomerByEmailQuery getCustomerByEmailQuery = new(req.Email);
        var customer = await sender.Send(getCustomerByEmailQuery, ct);

        if (customer is null || !AuthenticationService.VerifyPassword(req.Password, [.. customer.PasswordHash], [.. customer.PasswordSalt]))
        {
            await SendAsync(new LoginResponse(LoginResponseStatus.Error, LoginResponseErrorType.InvalidCredentials), cancellation: ct);
            return;
        }
        if (!customer.Active)
        {
            await SendAsync(new LoginResponse(LoginResponseStatus.Error, LoginResponseErrorType.AccountNotActive), cancellation: ct);
            return;
        }
        if (customer.Deleted)
        {
            await SendAsync(new LoginResponse(LoginResponseStatus.Error, LoginResponseErrorType.AccountDeleted), cancellation: ct);
            return;
        }
        var tokeResult = authenticationService.GetTokenResult(customer.Id, customer.Email);
        UpdateCustomerRefreshTokenCommand updateCustomerRefreshTokenCommand = new(customer.Id, tokeResult.RefreshToken, tokeResult.RefreshTokenExpiry);
        await sender.Send(updateCustomerRefreshTokenCommand, ct);

        await SendOkAsync(new LoginResponse(LoginResponseStatus.Success,
                                            Token: tokeResult.Token,
                                            RefreshToken: tokeResult.RefreshToken), cancellation: ct);
    }
}
