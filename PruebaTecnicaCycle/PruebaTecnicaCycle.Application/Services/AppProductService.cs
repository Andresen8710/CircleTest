using AutoMapper;
using PruebaTecnicaCycle.Application.Interfaces;
using PruebaTecnicaCycle.Domain.Dtos.Request;
using PruebaTecnicaCycle.Domain.Dtos.Response;
using PruebaTecnicaCycle.Domain.Entities;
using PruebaTecnicaCycle.Domain.Repositories;

namespace PruebaTecnicaCycle.Application.Services
{
    public class AppProductService : IAppProductService
    {
        private readonly IMapper _mapper;
        private readonly IDomainProductRepository _repository;

        public AppProductService(IMapper mapper, IDomainProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ProductReponseDto> AddAsync(ProductRequestModelDto productRequestModelDto)
        {
            var newProduct = _mapper.Map<Product>(productRequestModelDto);

            var product = await _repository.AddAsync(newProduct);

            var result = _mapper.Map<ProductReponseDto>(product);

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resultDelete = false;

            var product = await _repository.GetByIdAsync(id);

            if (product == null) return false;

            resultDelete = await _repository.DeleteAsync(product);

            return resultDelete;
        }

        public async Task<List<ProductReponseDto>> GetAllDapperAsync()
        {
            var productList = await _repository.GetAllDapperAsync();

            var result = _mapper.Map<List<ProductReponseDto>>(productList);

            return result;
        }

        public async Task<List<ProductReponseDto>> GetAllEntityFrameworkAsync()
        {
            var productList = await _repository.GetAllEntityFrameworkAsync();

            var result = _mapper.Map<List<ProductReponseDto>>(productList);

            return result;
        }

        public async Task<ProductReponseDto> GetByIdAsync(int Id)
        {
            var product = await _repository.GetByIdAsync(Id);

            if (product == null) return null;

            var result = _mapper.Map<ProductReponseDto>(product);

            return result;
        }

        public async Task<bool> UpdateAsync(int id, ProductRequestModelDto product)
        {
            var productToEdit = await _repository.GetByIdAsync(id);

            if (productToEdit == null) return false;

            _mapper.Map(product, productToEdit, typeof(ProductRequestModelDto), typeof(Product));

            var result = await _repository.UpdateAsync(productToEdit);

            return result;
        }
    }
}