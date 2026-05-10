using SistemaGestionAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAPI.DTOs
{
    public class ClienteDTO : ClienteCreacionDTO
    {
        public int Id { get; set; }
    
        public string Name { get; set; }
       
        public string Correo { get; set; }
        public string Numero { get; set; }

    }
}
