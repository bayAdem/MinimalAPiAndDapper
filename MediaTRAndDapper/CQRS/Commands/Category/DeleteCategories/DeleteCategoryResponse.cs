namespace MediaTRAndDapper.CQRS.Commands.Category.DeleteCategories
{
    public class DeleteCategoryResponse(bool success, string message)
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
    }
}
