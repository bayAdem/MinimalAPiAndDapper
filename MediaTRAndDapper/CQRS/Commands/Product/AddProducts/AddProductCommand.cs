using MediaTRAndDapper.Common.ICommand;

namespace MediaTRAndDapper.CQRS.Commands.Product.AddProducts;

public sealed record AddProductCommand(
                                        int Id,
                                        string Name,
                                        string Description,
                                        decimal Price,
                                        int Stock,
                                        int CategoryId
                                        ) : ICommand;
