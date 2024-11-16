using MediaTRAndDapper.Models;

namespace Platform.Api.Database.Repositories.Abstract;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

    Task<ICollection<Product>> GetProductsByIdsAsync(ICollection<int> productIds);
}
