using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionAPI.DTOs;
using SistemaGestionAPI.Entities;
using SistemaGestionAPI.Interfaces;

namespace SistemaGestionAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/ventas")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService service;
        private readonly IMapper _mapper;

        public VentasController(
            IVentaService service,
            IMapper mapper)
        {
            this.service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<VentaDTO>>> Get()
        {
            var entidades = await service.GetAll();

            var dtos = _mapper.Map<List<VentaDTO>>(entidades);

            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenerVenta")]
        public async Task<ActionResult<VentaDTO>> Get(int id)
        {
            var entidad = await service.GetById(id);

            if (entidad == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<VentaDTO>(entidad);

            return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(
            [FromBody] VentaCreacionDTO dto)
        {
            var entidad = _mapper.Map<Venta>(dto);

            entidad.Fecha = DateTime.Now;

            await service.Add(entidad);

            var ventaDTO = _mapper.Map<VentaDTO>(entidad);

            return new CreatedAtRouteResult(
                "obtenerVenta",
                new { id = ventaDTO.Id },
                ventaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(
            int id,
            [FromBody] VentaCreacionDTO dto)
        {
            var entidad = _mapper.Map<Venta>(dto);

            entidad.Id = id;

            var existe = await service.GetById(id);

            if (existe == null)
            {
                return NotFound();
            }

            await service.Update(entidad);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await service.GetById(id);

            if (existe == null)
            {
                return NotFound();
            }

            await service.Delete(id);

            return NoContent();
        }
    }
}