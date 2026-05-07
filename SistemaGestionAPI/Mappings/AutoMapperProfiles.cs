using AutoMapper;
using SistemaGestionAPI.DTOs;
using SistemaGestionAPI.SistemaGestion.Entities;

namespace SistemaGestionAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
        
        CreateMap<Producto, ProductoDTO>().ReverseMap();
        CreateMap<ProductoCreacionDTO, Producto>();
        }
    }
}
