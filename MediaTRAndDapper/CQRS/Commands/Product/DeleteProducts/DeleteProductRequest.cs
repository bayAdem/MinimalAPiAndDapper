namespace MediaTRAndDapper.CQRS.Commands.Product.DeleteProducts;

public class DeleteProductRequest(int Id)
{
    public int Id { get; } = Id;
}
