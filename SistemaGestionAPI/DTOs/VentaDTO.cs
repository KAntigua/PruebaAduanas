using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAPI.DTOs
{
    public class VentaDTO :  VentaCreacionDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string ListaProductos { get; set; }
        [Required]
        public double Total { get; set; }
        public int Cantidad { get; set; }

    }
    
}
