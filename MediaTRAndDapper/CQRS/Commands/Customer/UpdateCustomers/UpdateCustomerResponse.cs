namespace MediaTRAndDapper.CQRS.Commands.Customer.UpdateCustomers;

public class UpdateCustomerResponse(bool success, string message)
{
    public bool Success { get; set; } = success;
    public string Message { get; set; } = message;
}
