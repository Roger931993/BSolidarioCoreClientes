namespace Core.Clientes.Domain.Entities
{
    public class contacto : BaseEntity
    {
        public int contacto_id { get; set; }
        public int? cliente_id { get; set; }
        public string? descripcion { get; set; }
        public string? email { get; set; }
        public string? telefono { get; set; }

        public cliente? cliente { get; set; }
    }
}
