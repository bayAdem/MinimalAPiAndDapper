using Dapper;
using MediaTRAndDapper.Database.DPContext;
using MediaTRAndDapper.Models;
using Platform.Api.Database.Repositories.Abstract;

namespace Platform.Api.Database.Repositories.Concrete;

public class OrderRepository : IOrderRepository
{
    private readonly DapperContext _context;

    public OrderRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var query = @"SELECT * FROM Orders";
        return await connection.QueryAsync<Order>(query);
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();

        DynamicParameters param = new();
        param.Add("OrderId", id);
        var query = @"SELECT * FROM OrderDetails WHERE Id = @OrderId";
        return await connection.QueryFirstOrDefaultAsync<Order>(query, param, commandTimeout: 500);
    }

    public async Task AddAsync(Order order)
    {
        using var connection = _context.CreateConnection();

        DynamicParameters param = new();
        param.Add("CustomerId", order.CustomerId);
        param.Add("OrderDate", order.OrderDate);
        param.Add("TotalAmount", order.TotalAmount);
        param.Add("Status", order.Status);

        var query = @"
        INSERT INTO Orders (CustomerId, OrderDate, TotalAmount, Status) 
        VALUES (@CustomerId, @OrderDate, @TotalAmount, @Status)";

        await connection.ExecuteAsync(query, param, commandTimeout: 500);
    }
    public async Task UpdateAsync(Order order)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Id", order.Id);
        param.Add("CustomerId", order.CustomerId);
        param.Add("TotalAmount", order.TotalAmount);
        param.Add("Status", order.Status);

        var query = @"UPDATE Orders SET CustomerId = @CustomerId, TotalAmount = @TotalAmount, Status = @Status WHERE Id = @Id";
        await connection.ExecuteAsync(query, param, commandTimeout: 500);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Id", id);

        var query = @"DELETE FROM Orders WHERE Id = @Id";
        await connection.ExecuteAsync(query, param, commandTimeout: 500);
    }
    public async Task<Order> GetOrderWithCustomerDetailsAsync(int orderId)
    {
        using var connection = _context.CreateConnection();

        var query = @"
            SELECT 
                o.Id AS OrderId, 
                o.OrderDate, 
                c.FullName AS CustomerFullName, 
                c.Address AS CustomerAddress 
            FROM Orders o
            JOIN Customers c ON o.CustomerId = c.Id
            WHERE o.Id = @OrderId";

        // DynamicParameters oluşturma
        var param = new DynamicParameters();
        param.Add("Id", orderId);

        var order = await connection.QuerySingleOrDefaultAsync<Order>(query, param, commandTimeout: 500);
        return order;
    }
}
