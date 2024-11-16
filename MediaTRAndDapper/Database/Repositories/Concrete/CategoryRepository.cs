using Dapper;
using MediaTRAndDapper.Database.DPContext;
using MediaTRAndDapper.Models;
using Platform.Api.Database.Repositories.Abstract;

namespace Platform.Api.Database.Repositories.Concrete;

public class CategoryRepository : ICategoryRepository
{
    private readonly DapperContext _context;

    public CategoryRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM Categories";
        return await connection.QueryAsync<Category>(query, commandTimeout: 500);
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();

        DynamicParameters param = new();
        param.Add("Id", id);

        var query = @"SELECT c.*
                        FROM dbo.Categories c
                        INNER JOIN dbo.Products p ON c.Id = p.CategoryId
                        WHERE c.Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Category>(query, param, commandTimeout: 500);
    }

    public async Task<Category> AddAsync(Category category)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Name", category.Name);
        param.Add("Description", category.Description);
        param.Add("CreatedAt", category.CreatedAt == default ? DateTime.Now : category.CreatedAt);

        var query = "INSERT INTO Categories (Name, Description, CreatedAt) VALUES (@Name, @Description, @CreatedAt)";

        var result = await connection.ExecuteAsync(query, param, commandTimeout: 500);
        if (result > 0)
        {
            return category; 
        }
        return null; 
    }


    public async Task UpdateAsync(Category category)
    {
        using var connection = _context.CreateConnection();
        DynamicParameters param = new();
        param.Add("Id",category.Id);
        param.Add("Name", category.Name);
        param.Add("Description", category.Description);
        param.Add("Products", category.Products);
        var query = "UPDATE Categories SET Name = @Name, Description=@Description,Products = @Products WHERE Id = @Id";
        await connection.ExecuteAsync(query, param, commandTimeout: 500);
    }

    public async Task<Category> QueryAsync(string name)
    {
      using var connection = _context.CreateConnection();
    var query = "SELECT * FROM Categories WHERE Name = @Name";
    var result = await connection.QuerySingleOrDefaultAsync<Category>(query, new { Name = name });
    return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();

        DynamicParameters param = new();
        param.Add("Id", id);

        var query = "DELETE FROM Categories WHERE Id = @Id";
        await connection.ExecuteAsync(query, param, commandTimeout: 500);
        return true;
    }

    public async Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<Guid> productId)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM Products WHERE Id IN @Ids";
        return await connection.QueryAsync<Product>(query, new { Ids = productId });
    }

}
