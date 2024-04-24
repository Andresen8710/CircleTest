using AutoMapper;
using PruebaTecnicaCycle.Domain.Dtos.Request;
using PruebaTecnicaCycle.Domain.Dtos.Response;
using PruebaTecnicaCycle.Domain.Entities;

namespace PruebaTecnicaCycle.Infraestructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ProductRequestModelDto, Product>();
            CreateMap<Product, ProductReponseDto>();
        }
    }
}