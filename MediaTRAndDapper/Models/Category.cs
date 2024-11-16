using MediaTRAndDapper.Models.BaseEntity;

namespace MediaTRAndDapper.Models;

public class Category : BaseEntity<int>
{
    public string Name { get; set; } 
    public string Description { get; set; }
    public DateTime? CreatedAt { get; set; } 
    public ICollection<Product> Products { get; set; }
      
}
