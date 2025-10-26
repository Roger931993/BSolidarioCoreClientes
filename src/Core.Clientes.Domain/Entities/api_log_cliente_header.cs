namespace Core.Clientes.Domain.Entities
{
    public class api_log_cliente_header
    {
        public int api_log_cliente_header_id { get; set; }
        public string? request_method { get; set; }
        public string? request_url { get; set; }
        public int? response_code { get; set; }
        public Guid id_tracking { get; set; }
        public DateTime? created_at { get; set; }

        // Relación con ApiLogsDetail
        public ICollection<api_log_cliente_detail>? api_log_cliente_detail { get; set; }
    }
}
