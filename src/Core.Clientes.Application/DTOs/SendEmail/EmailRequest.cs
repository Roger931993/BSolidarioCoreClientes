namespace Core.Clientes.Application.DTOs.SendEmail
{
    public class EmailRequest
    {
        public List<ToEmailDto> ToEmail { get; set; } = new List<ToEmailDto>();
        public string Subject { get; set; }
        public string Message { get; set; }
        public List<(byte[] content, string fileName, string contentType)> Attachments { get; set; }
    }

    public class ToEmailDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
