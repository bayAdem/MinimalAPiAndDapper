using MediaTRAndDapper.Models;

namespace Platform.Api.Database.Repositories.Abstract;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> GetByIdAsync(int id);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(int id);

    Task UpdateRefreshTokenAsync(int Id, string? refreshToken = null, DateTime? refreshTokenExpiry = null);

    Task<Customer> GetCustomerEmailAsync(string email);
}
