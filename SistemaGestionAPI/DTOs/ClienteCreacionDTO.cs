using SistemaGestionAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAPI.DTOs
{
    public class ClienteCreacionDTO
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        public string Numero { get; set; }



    }
}
