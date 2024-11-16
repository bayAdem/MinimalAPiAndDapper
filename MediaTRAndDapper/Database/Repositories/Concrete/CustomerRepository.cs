using Dapper;
using MediaTRAndDapper.Database.DPContext;
using MediaTRAndDapper.Models;
using Platform.Api.Database.Repositories.Abstract;
using System.Text.Json;
using System.Xml.Linq;

namespace Platform.Api.Database.Repositories.Concrete;

public class CustomerRepository : ICustomerRepository
{
    private readonly DapperContext _context;

    public CustomerRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM Customers";
        return await connection.QueryAsync<Customer>(query);
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Id", id);
        var query = "SELECT dbo.Orders, dbo.Customers FROM dbo.Customers INNER JOIN dbo.Orders ON dbo.Customers.Id = dbo.Orders.CustomerId WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Customer>(query, param, commandTimeout: 500);
    }

    public async Task AddAsync(Customer customer)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("FullName", customer.FullName);
        param.Add("Email", customer.Email);
        param.Add("PhoneNumber", customer.PhoneNumber);
        param.Add("Address", customer.Address);

        string ordersJson = JsonSerializer.Serialize(customer.Orders);
        param.Add("Orders", ordersJson);

        param.Add("CreatedAt", customer.CreatedAt == default ? DateTime.Now : customer.CreatedAt);

        var query = "INSERT INTO Customers (FullName, Email, PhoneNumber, Address, CreatedAt, Orders) VALUES (@FullName, @Email, @PhoneNumber, @Address, @CreatedAt, @Orders)";
        await connection.ExecuteAsync(query, param, commandTimeout: 500);
    }

    public async Task UpdateAsync(Customer customer)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Id", customer.Id);
        param.Add("FullName", customer.FullName);
        param.Add("Email", customer.Email);
        param.Add("PhoneNumber", customer.PhoneNumber);
        param.Add("Address", customer.Address);
        string ordersJson = JsonSerializer.Serialize(customer.Orders);
        param.Add("Orders", ordersJson);

        param.Add("CreatedAt", customer.CreatedAt == default ? DateTime.Now : customer.CreatedAt);

        var query = "UPDATE Customers SET FullName = @FullName, Email = @Email,PhoneNumber=@PhoneNumber, Address=@Address, Orders=@Orders,CreatedAt=@CreatedAt WHERE Id = @Id";
        await connection.ExecuteAsync(query, param, commandTimeout: 500);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Id", id);

        var query = "DELETE FROM Customers WHERE Id = @Id";
        await connection.ExecuteAsync(query, param, commandTimeout: 500);
    }

    public async Task<Customer> GetCustomerEmailAsync(string email)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM Customers WHERE Email = @Email";
        var result = await connection.QuerySingleOrDefaultAsync<Customer>(query, new { Email = email });
        return result;
    }

    public Task UpdateRefreshTokenAsync(int userId, string? refreshToken = null, DateTime? refreshTokenExpiry = null)
    {
        using var connection = _context.CreateConnection();
        //var query = "Update"

        return null; 
    }
}
