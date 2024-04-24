using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PruebaTecnicaCycle.Domain.Dtos.Identity;
using PruebaTecnicaCycle.Identity.Models;

namespace PruebaTecnicaCycle.Identity.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<RegistrationRequest, ApplicationUser>()
            //  .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            //  .ForMember(dest => dest.PasswordHash,opt=>opt.MapFrom(src=>src.Password))
            //  .ForMember(dest => dest.Name, opt=>opt.MapFrom(src=>src.Name))
            //  .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            //  .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //  .ForMember(dest => dest.NormalizedUserName,opt=>opt.MapFrom(src => src.UserName.ToLower()));

            CreateMap<RegistrationRequest, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpper()));// Aseguramos que el UserName esté normalizado
            //.ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => new PasswordHasher<ApplicationUser>().HashPassword(null, src.Password)));

            CreateMap<ApplicationUser, RegistrationResponse>();
        }
    }
}