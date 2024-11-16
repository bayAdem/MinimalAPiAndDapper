using MediaTRAndDapper.Models.BaseEntity;

namespace MediaTRAndDapper.Models;

public class Order : BaseEntity<int>
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } 
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } 
    public ICollection<OrderDetail> OrderDetails { get; set; } 
}
