namespace SistemaGestionAPI.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Correo { get; set; }
        public int Numero { get; set; }

        public ICollection<Venta> Ventas { get; set; }

    }
}
