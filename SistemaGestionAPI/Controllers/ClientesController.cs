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
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService service;
        private readonly IMapper _mapper;

        public ClientesController(
            IClienteService service,
            IMapper mapper)
        {
            this.service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteDTO>>> Get()
        {
            var entidades = await service.GetAll();

            var dtos = _mapper.Map<List<ClienteDTO>>(entidades);

            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenerCliente")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            var entidad = await service.GetById(id);

            if (entidad == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<ClienteDTO>(entidad);

            return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(
            [FromBody] ClienteCreacionDTO dto)
        {
            var entidad = _mapper.Map<Cliente>(dto);

            await service.Add(entidad);

            var clienteDTO = _mapper.Map<ClienteDTO>(entidad);

            return new CreatedAtRouteResult(
                "obtenerCliente",
                new { id = clienteDTO.Id },
                clienteDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(
            int id,
            [FromBody] ClienteCreacionDTO dto)
        {
            var entidad = _mapper.Map<Cliente>(dto);

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