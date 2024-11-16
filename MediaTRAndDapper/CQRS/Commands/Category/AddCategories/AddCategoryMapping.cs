namespace MediaTRAndDapper.CQRS.Commands.Category.AddCategories;

public static class AddCategoryMapping
{
    public static AddCategoryCommand AddCategoryCommand(AddCategoryRequest _addCategoryRequest, int Id)
    {
        ArgumentNullException.ThrowIfNull(_addCategoryRequest, nameof(_addCategoryRequest));

        return new AddCategoryCommand(
            _addCategoryRequest.Id,
                _addCategoryRequest.Name,
                _addCategoryRequest.Description,
                _addCategoryRequest.CreatedAt,
                _addCategoryRequest.ProductId
        );
    }
}
