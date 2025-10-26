using Core.Clientes.Application.DTOs;
using Core.Clientes.Application.DTOs.Base;
using MediatR;

namespace Core.Clientes.Application.Features.Contact.Queries
{
    public record GetContactQuery(RequestBase<GetContactRequest> request) : IRequest<ResponseBase<GetContactResponse>>;

    public class GetContactRequest
    {
        public int? cliente_id { get; set; }
        public int? contacto_id { get; set; }
    }

    public class GetContactResponse
    {
        public List<contactoDto>? contacto { get; set; }
    }
}
