namespace MediaTRAndDapper.CQRS.Commands.Product.DeleteProducts;

public class DeleteProdctResponse(bool success, string message)
{
    public bool Success { get; set; } = success;
    public string Message { get; set; } = message;
}
