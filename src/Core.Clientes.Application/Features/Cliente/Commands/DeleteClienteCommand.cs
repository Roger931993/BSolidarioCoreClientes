using Core.Clientes.Application.DTOs;
using Core.Clientes.Application.DTOs.Base;
using MediatR;

namespace Core.Clientes.Application.Features.Cliente.Commands
{
    public class DeleteClienteCommand : RequestBase<DeleteClienteRequest>, IRequest<ResponseBase<DeleteClienteResponse>>
    {
    }

    public class DeleteClienteRequest
    {
        public int cliente_id { get; set; }
    }

    public class DeleteClienteResponse
    {
        public clienteDto? cliente { get; set; }
    }
}
