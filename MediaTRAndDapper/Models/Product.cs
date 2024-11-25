using MediaTRAndDapper.Models.BaseEntity;

namespace MediaTRAndDapper.Models;

public class Product : BaseEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }

}
