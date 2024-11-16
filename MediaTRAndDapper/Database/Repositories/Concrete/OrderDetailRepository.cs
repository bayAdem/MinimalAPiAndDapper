using Dapper;
using MediaTRAndDapper.Database.DPContext;
using MediaTRAndDapper.Models;
using Platform.Api.Database.Repositories.Abstract;

namespace Platform.Api.Database.Repositories.Concrete;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly DapperContext _context;

    public OrderDetailRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderDetail>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var query = @"SELECT * FROM OrderDetails";
        return await connection.QueryAsync<OrderDetail>(query);
    }

    public async Task<OrderDetail> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Id", id);
        var query = @"SELECT * FROM OrderDetails WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<OrderDetail>(query, param, commandTimeout: 500);
    }

    public async Task AddAsync(OrderDetail orderDetail)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("ProductId", orderDetail.ProductId);
        param.Add("Quantity", orderDetail.Quantity);
        param.Add("UnitPrice", orderDetail.UnitPrice);
        param.Add("TotalPrice", orderDetail.TotalPrice);

        var query = @"INSERT INTO OrderDetails (ProductId, OrderDate,UnitPrice,TotalPrice) VALUES (@ProductId, @OrderDate,@UnitPrice,@TotalPrice)";
        await connection.ExecuteAsync(query, param, commandTimeout: 500);
    }

    public async Task UpdateAsync(OrderDetail orderDetail)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("ProductId", orderDetail.ProductId);
        param.Add("Quantity", orderDetail.Quantity);
        param.Add("UnitPrice", orderDetail.UnitPrice);
        param.Add("TotalPrice", orderDetail.TotalPrice);

        var query = @"UPDATE OrderDetails SET ProductId= @ProductId Quantity = @Quantity, UnitPrice = @UnitPrice,TotalPrice = @TotalPrice  WHERE Id = @Id";
        await connection.ExecuteAsync(query, param, commandTimeout: 500);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Id", id);
        var query = @"DELETE FROM OrderDetails WHERE Id = @Id";


        await connection.ExecuteAsync(query, param, commandTimeout: 500);
    }
}
