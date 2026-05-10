using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionAPI.DTOs;
using SistemaGestionAPI.Interfaces;
using SistemaGestionAPI.SistemaGestion.Entities;

namespace SistemaGestionAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService service;
        private readonly IMapper _mapper;

        public ProductosController(
            IProductoService service,
            IMapper mapper)
        {
            this.service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoDTO>>> Get()
        {
            var entidades = await service.GetAll();

            var dtos = _mapper.Map<List<ProductoDTO>>(entidades);

            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenerProducto")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var entidad = await service.GetById(id);

            if (entidad == null)
            {
                return NotFound(new
                {
                    mensaje = "Producto no encontrado"
                });
            }

            var dto = _mapper.Map<ProductoDTO>(entidad);

            return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(
            [FromBody] ProductoCreacionDTO productoCreacionDTO)
        {
            var entidad = _mapper.Map<Producto>(productoCreacionDTO);

            await service.Add(entidad);

            var productoDTO = _mapper.Map<ProductoDTO>(entidad);

            return new CreatedAtRouteResult(
                "obtenerProducto",
                new { id = productoDTO.Id },
                productoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(
            int id,
            [FromBody] ProductoCreacionDTO productoCreacionDTO)
        {
            var entidad = _mapper.Map<Producto>(productoCreacionDTO);

            entidad.Id = id;

            var existe = await service.GetById(id);

            
            if (existe == null)
            {
                return NotFound(new
                {
                    mensaje = "Producto no encontrado"
                });
            }

            await service.Update(entidad);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await service.GetById(id);

            if (existe == null)
            {
                return NotFound(new
                {
                    mensaje = "Producto no encontrado"
                });
            }

            await service.Delete(id);

            return NoContent();
        }
    }
}