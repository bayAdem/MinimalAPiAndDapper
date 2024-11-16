using MediaTRAndDapper.Common.ICommand;

namespace MediaTRAndDapper.CQRS.Commands.Category.UpdateCategories;

public sealed record UpdateCategoryCommand(int Id,
    string Name,
    string Description,
    DateTime? CreatedAt,
    ICollection<int> ProductId) : ICommand;


