using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAPI.DTOs
{
    public class ProductoDTO : ProductoCreacionDTO
    {
        public int Id { get; set; }
      
        public string Nombre { get; set; }

        public decimal Precio { get; set; }
        public string Descripcion { get; set; }

        public int Stock { get; set; }
    }
}
