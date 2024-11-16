using MediaTRAndDapper.Models;

namespace Platform.Api.Database.Repositories.Abstract;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(int id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(int id);
    Task<Order> GetOrderWithCustomerDetailsAsync(int orderId);
}