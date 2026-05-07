using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAPI.Entities
{
    public class Venta
    {
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string ListaProductos { get; set; }
        [Required]
        public double Total { get; set; }


        public ICollection<VentaProducto> VentaProductos { get; set; }
    }
}
