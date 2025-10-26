using Core.Clientes.Application.DTOs;
using Core.Clientes.Application.DTOs.Base;
using MediatR;

namespace Core.Clientes.Application.Features.Cliente.Queries
{
    public record GetClienteQuery(RequestBase<GetClienteRequest> request) : IRequest<ResponseBase<GetClienteResponse>>;

    public class GetClienteRequest
    {
        public int? cliente_id { get; set; }
        public string? identificacion { get; set; }
        public string? username { get; set; }

        public TypeGetCliente? TypeGetCliente { get; set; }

    }

    public class GetClienteResponse
    {
        public List<clienteDto>? cliente { get; set; }
    }

    public enum TypeGetCliente
    {
        None,
        ById,                
        ByIdentificacion,
        ByUserName

    }
}
