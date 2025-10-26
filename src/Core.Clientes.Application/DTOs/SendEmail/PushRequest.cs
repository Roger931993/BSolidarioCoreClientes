namespace Core.Clientes.Application.DTOs.SendEmail
{
    public class PushRequest
    {
        public string? email { get; set; }
        public string? name_email { get; set; }
        public string? subject { get; set; }
        public List<AttachmentsDto>? attachments { get; set; }
        public string? message { get; set; }
    }

    public class AttachmentsDto
    {
        public byte[]? content { get; set; }
        public string? fileName { get; set; }
        public string? contentType { get; set; }
    }
}
