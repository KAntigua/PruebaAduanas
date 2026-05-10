using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAPI.DTOs
{
    public class VentaCreacionDTO
    {
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        [Required]
        public string ListaProductos { get; set; }
        [Required]
        public double Total { get; set; }
    }
}
