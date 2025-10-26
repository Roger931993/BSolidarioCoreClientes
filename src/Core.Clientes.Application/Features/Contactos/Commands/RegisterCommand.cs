using Core.Clientes.Application.DTOs;
using Core.Clientes.Application.DTOs.Base;
using MediatR;

namespace Core.Clientes.Application.Features.Contact.Commands
{
    public class RegisterCommand : RequestBase<RegisterContactRequest>, IRequest<ResponseBase<RegisterContactResponse>>
    {
    }

    public class RegisterContactRequest
    {
        public int? contacto_id { get; set; }
        public int? cliente_id { get; set; }
        public string? descripcion { get; set; }
        public string? email { get; set; }
        public string? telefono { get; set; }
    }

    public class RegisterContactResponse
    {
        public contactoDto? contacto { get; set; }
    }
}
