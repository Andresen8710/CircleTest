using PruebaTecnicaCycle.Domain.Entities;

namespace PruebaTecnicaCycle.Domain.Repositories
{
    public interface IDomainProductRepository
    {
        Task<List<Product>> GetAllDapperAsync();

        Task<List<Product>> GetAllEntityFrameworkAsync();

        Task<Product> GetByIdAsync(int Id);

        Task<Product> AddAsync(Product product);

        Task<bool> UpdateAsync(Product product);

        Task<bool> DeleteAsync(Product product);
    }
}