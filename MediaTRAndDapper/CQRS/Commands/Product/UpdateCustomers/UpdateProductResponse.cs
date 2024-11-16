namespace MediaTRAndDapper.CQRS.Commands.Product.UpdateCustomers;

public class UpdateProductResponse(bool success, string message)
{
    public bool Success { get; set; } = success;
    public string Message { get; set; } = message; 
}