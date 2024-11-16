using MediaTRAndDapper.Models;

namespace Platform.Api.Database.Repositories.Abstract;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(int id);
    Task<Category> AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<Guid> productId);
    Task<Category> QueryAsync(string name);
    Task<bool> DeleteAsync(int id);

}