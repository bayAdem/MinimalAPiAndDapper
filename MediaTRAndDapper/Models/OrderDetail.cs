using MediaTRAndDapper.Models.BaseEntity;

namespace MediaTRAndDapper.Models
{
    public class OrderDetail : BaseEntity<int>
    {
        public int Id { get; set; } 
        public Order Order { get; set; }
        public int ProductId { get; set; } 
        public Product Product { get; set; } 
        public int Quantity { get; set; } 
        public decimal UnitPrice { get; set; } 
        public decimal TotalPrice { get; set; } // Toplam fiyat (Quantity * UnitPrice)
    }
}
