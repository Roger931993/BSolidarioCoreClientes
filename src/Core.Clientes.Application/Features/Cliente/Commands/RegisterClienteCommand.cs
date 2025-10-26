using Core.Clientes.Application.DTOs;
using Core.Clientes.Application.DTOs.Base;
using MediatR;

namespace Core.Clientes.Application.Features.Cliente.Commands
{
    public class RegisterClienteCommand : RequestBase<RegisterClienteRequest>, IRequest<ResponseBase<RegisterClienteResponse>>
    {
    }

    public class RegisterClienteRequest
    {
        public int? cliente_id { get; set; }
        public string? primer_nombre { get; set; }
        public string? segundo_nombre { get; set; }
        public string? apellido_paterno { get; set; }
        public string? apellido_materno { get; set; }
        public string? identificacion { get; set; }
        public string? username { get; set; }
        public int? estado { get; set; }
    }

    public class RegisterClienteResponse
    {
        public clienteDto? cliente { get; set; }
    }
}
