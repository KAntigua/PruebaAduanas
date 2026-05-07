using SistemaGestionAPI.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGestionAPI.SistemaGestion.Entities
{
    public class Producto
    {

        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public int Stock { get; set; }

        public ICollection<VentaProducto> VentaProductos { get; set; }

    }
}
