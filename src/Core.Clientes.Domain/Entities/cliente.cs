namespace Core.Clientes.Domain.Entities
{
    public class cliente : BaseEntity
    {
        public int cliente_id { get; set; }
        public string? primer_nombre { get; set; }
        public string? segundo_nombre { get; set; }
        public string? apellido_paterno { get; set; }
        public string? apellido_materno { get; set; }
        public string? identificacion { get; set; }        
        public string? username { get; set; }        
     

        public List<contacto>? contacto { get; set; }        
    }
}
