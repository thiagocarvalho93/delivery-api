using AutoMapper;
using DeliveryApi.Domain.DTOs.Produto;
using DeliveryApi.Domain.Models;

namespace DeliveryApi.Domain.Mapper
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoRequestDTO>().ReverseMap();
            CreateMap<Produto, ProdutoResponseDTO>().ReverseMap();
        }
    }
}