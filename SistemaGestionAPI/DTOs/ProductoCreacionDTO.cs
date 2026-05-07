using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAPI.DTOs
{
    public class ProductoCreacionDTO
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
