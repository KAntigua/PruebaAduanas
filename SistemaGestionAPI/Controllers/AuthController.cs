using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaGestionAPI.DTOs;
using SistemaGestionAPI.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaGestionAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public AuthController(
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [HttpPost("registro")]
        public async Task<ActionResult> Registro(
            UsuarioCreacionDTO dto)
        {
            var usuario = new Usuario
            {
                Username = dto.Username,
                Password = dto.Password
            };

            context.Add(usuario);

            await context.SaveChangesAsync();

            return Ok("Usuario registrado");
        }

        [HttpPost("login")]
        public ActionResult Login(LoginDTO dto)
        {
            var usuario = context.Usuarios
                .FirstOrDefault(x =>
                    x.Username == dto.Username &&
                    x.Password == dto.Password);

            if (usuario == null)
            {
                return BadRequest("Credenciales incorrectas");
            }

            var claims = new List<Claim>
            {
                new Claim("username", usuario.Username)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["jwt:key"]));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler()
                    .WriteToken(token)
            });
        }
    }
}