namespace MediaTRAndDapper.CQRS.Commands.Category.UpdateCategories;

public class UpdateCategoryResponse(bool success, string message)
{
    public bool Success { get; set; } = success;
    public string Message { get; set; } = message;
}
