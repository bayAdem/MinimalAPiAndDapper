using MediaTRAndDapper.Common.ICommand;

namespace MediaTRAndDapper.CQRS.Commands.Product.DeleteProducts;

public class DeleteProductCommand(int Id) : ICommand
{
    public int Id { get; } = Id;
}
