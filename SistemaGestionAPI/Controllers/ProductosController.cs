using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionAPI.DTOs;
using SistemaGestionAPI.SistemaGestion.Entities;

namespace SistemaGestionAPI.Controllers
{

    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper _mapper;
        public ProductosController(ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;

        }


        [HttpGet]
        public async Task<ActionResult<List<ProductoDTO>>> Get()
        {
            var entidades = await context.Productos.ToListAsync();
            var dtos = _mapper.Map<List<ProductoDTO>>(entidades);
            return dtos;
        }


        [HttpGet("{id:int}", Name = "obtenerProducto")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var entidad = await context.Productos.FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null) {
                return NotFound();
            }

            var dto = _mapper.Map<ProductoDTO>(entidad);

            return dto;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductoCreacionDTO productoCreacionDTO)
        {
            var entidad = _mapper.Map<Producto>(productoCreacionDTO);
            context.Add(entidad);
            await context.SaveChangesAsync();

            var productoDTO = _mapper.Map<ProductoDTO>(entidad);

            return new CreatedAtRouteResult("obtenerProducto", new { id = productoDTO.Id }, productoDTO);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductoCreacionDTO productoCreacionDTO)
        {
            var entidad = _mapper.Map<Producto>(productoCreacionDTO);
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            var existe =  await context.Productos.AnyAsync(x => x.Id == id);

            if(!existe)
            {
                return NotFound();
            }

            context.Remove(new Producto() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }


    }
}
