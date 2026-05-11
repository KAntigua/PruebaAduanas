using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAPI.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Rol { get; set; }

    }
}
