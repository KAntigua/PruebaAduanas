using AutoMapper;
using SistemaGestionAPI.DTOs;
using SistemaGestionAPI.Entities;
using SistemaGestionAPI.SistemaGestion.Entities;



namespace SistemaGestionAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
        
        CreateMap<Producto, ProductoDTO>().ReverseMap();
        CreateMap<ProductoCreacionDTO, Producto>();


        CreateMap<Cliente, ClienteDTO>();
        CreateMap<ClienteCreacionDTO, Cliente>();

            CreateMap<Venta, VentaDTO>();
            CreateMap<VentaCreacionDTO, Venta>();

        }
    }
}
