using Dapper;
using MediaTRAndDapper.Database.DPContext;
using MediaTRAndDapper.Models;
using Platform.Api.Database.Repositories.Abstract;

namespace Platform.Api.Database.Repositories.Concrete;

public class ProductRepository : IProductRepository
{
    private readonly DapperContext _context;

    public ProductRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var query = @"SELECT * FROM Products";
        return await connection.QueryAsync<Product>(query);
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Id", id);

        var query = @"
         SELECT c.Id,c.Name, p.Id, p.Name,p.Price, p.Description FROM dbo.Categories c 
         INNER JOIN 
         dbo.Products p ON c.Id = p.CategoryId 
         WHERE  p.Id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Product>(query, param, commandTimeout: 500);
    }

    public async Task AddAsync(Product product)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Name", product.Name);
        param.Add("Description", product.Description);
        param.Add("Price", product.Price);
        param.Add("Stock", product.Stock);
        param.Add("CategoryId", product.CategoryId);
        var query = @"INSERT INTO Products (Name, Description,Price,Stock,CategoryId) VALUES (@Name ,@Description, @Price ,@Stock ,@CategoryId)";
        await connection.ExecuteAsync(query, param, commandTimeout: 500);
    }

    public async Task UpdateAsync(Product product)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Id", product.Id);
        param.Add("Name", product.Name);
        param.Add("Description", product.Description);
        param.Add("Price", product.Price);
        param.Add("Stock", product.Stock);
        param.Add("CategoryId", product.CategoryId);
        var query = @"UPDATE Products SET Name = @Name,Description=@Description, Price = @Price, Stock=@Stock, CategoryId = @CategoryId WHERE Id = @Id";
        await connection.ExecuteAsync(query, param, commandTimeout: 500);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Id", id);
        var query = @"DELETE FROM Products WHERE Id = @Id";
        await connection.ExecuteAsync(query, param, commandTimeout: 500);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Id", categoryId);

        var query = @"SELECT * FROM Products WHERE CategoryId = @CategoryId";
        return await connection.QueryAsync<Product>(query, param, commandTimeout: 500);
    }

    public async Task<ICollection<Product>> GetProductsByIdsAsync(ICollection<int> productIds)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM Products WHERE Id IN @ProductIds";
        return (await connection.QueryAsync<Product>(query, new { ProductIds = productIds })).ToList();
    }
}
