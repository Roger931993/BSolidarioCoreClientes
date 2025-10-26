namespace Core.Clientes.Domain.Entities
{
    public class api_log_cliente_detail
    {
        public int api_log_cliente_detail_id { get; set; }      
        public int? api_log_cliente_header_id { get; set; }// FK correcta
        public DateTime? created_at { get; set; }
        public int? status_code { get; set; }
        public string? type_process { get; set; }
        public string? data_message { get; set; }
        public string? process_component { get; set; }
        
        public api_log_cliente_header? api_log_cliente_header { get; set; }  // Propiedad de navegación

    }
}
