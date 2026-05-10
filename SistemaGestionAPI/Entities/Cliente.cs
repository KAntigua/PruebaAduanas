using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAPI.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        public string Numero { get; set; }

        public ICollection<Venta> Ventas { get; set; }

    }
}
