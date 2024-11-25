using MediaTRAndDapper.Common.ICommand;

namespace MediaTRAndDapper.CQRS.Commands.Category.AddCategories;

// Dto
public class AddCategoryCommand : ICommand<AddCategoryResponse<Models.Category>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public ICollection<int> ProductId { get; set; }

    public AddCategoryCommand(int id, string name, string description, DateTime? createdAt, ICollection<int> productId)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        ProductId = productId;
    }
}

