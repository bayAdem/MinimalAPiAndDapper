namespace MediaTRAndDapper.CQRS.Commands.Product.AddProducts;

public sealed record AddProductRequest(
                               int ProductId,
                               string Name,
                               string Description,
                               decimal Price,
                               int Stock,
                               int CategoryIds);
