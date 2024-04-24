using PruebaTecnicaCycle.Domain.Dtos.Request;
using PruebaTecnicaCycle.Domain.Dtos.Response;

namespace PruebaTecnicaCycle.Application.Interfaces
{
    public interface IAppProductService
    {
        Task<List<ProductReponseDto>> GetAllDapperAsync();

        Task<List<ProductReponseDto>> GetAllEntityFrameworkAsync();

        Task<ProductReponseDto> GetByIdAsync(int Id);

        Task<ProductReponseDto> AddAsync(ProductRequestModelDto productRequestModelDto);

        Task<bool> UpdateAsync(int id, ProductRequestModelDto product);

        Task<bool> DeleteAsync(int id);
    }
}