namespace MediaTRAndDapper.CQRS.Commands.Category.AddCategories
{
    public class AddCategoryResponse<T>(bool success, string message, T? Data)
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
        public T? Data { get; set; } = Data;
    }
}
