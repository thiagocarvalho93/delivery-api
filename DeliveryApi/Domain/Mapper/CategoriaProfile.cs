using AutoMapper;
using DeliveryApi.Domain.DTOs.Categoria;
using DeliveryApi.Domain.Models;

namespace DeliveryApi.Domain.Mapper
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<Categoria, CategoriaRequestDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaResponseDTO>().ReverseMap();
        }
    }
}