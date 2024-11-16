using MediaTRAndDapper.Models;

namespace MediaTRAndDapper.CQRS.Commands.Category.AddCategories;

public sealed record AddCategoryRequest(
    int Id,
    string Name,
    string Description,
    DateTime? CreatedAt,
    ICollection<int> ProductId
);


