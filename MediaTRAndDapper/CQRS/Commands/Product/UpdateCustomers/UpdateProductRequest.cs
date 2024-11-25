using MediatR;

namespace MediaTRAndDapper.CQRS.Commands.Product.UpdateCustomers;

public class UpdateProductRequest(int Id,
                                string Name,
                                string Description,
                                decimal Price,
                                int Stock,
                                int CategoryId
                                ) : IRequest<UpdateProductResponse>
{
    public int Id { get; } = Id;
    public string Name { get; } = Name;
    public string Description { get; } = Description;
    public decimal Price { get; } = Price;
    public int Stock { get; } = Stock;
    public int CategoryId { get; } = CategoryId;
}