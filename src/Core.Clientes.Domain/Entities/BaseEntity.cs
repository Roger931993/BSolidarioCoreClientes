namespace Core.Clientes.Domain.Entities
{
    public class BaseEntity
    {
        public int? estado { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }

    }
}
