namespace MediaTRAndDapper.CQRS.Commands.Product.AddProducts;

public static class AddProductMapping
{
    public static AddProductCommand ToAddProductComman(this AddProductRequest request, int Id)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        return new AddProductCommand(Id,
                                    request.Name,
                                    request.Description,
                                    request.Price,
                                    request.Stock,
                                    request.CategoryIds);
    }
};