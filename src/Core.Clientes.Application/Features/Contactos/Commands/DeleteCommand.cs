using Core.Clientes.Application.DTOs;
using Core.Clientes.Application.DTOs.Base;
using MediatR;

namespace Core.Clientes.Application.Features.Contact.Commands
{
    public class DeleteCommand : RequestBase<DeleteContactRequest>, IRequest<ResponseBase<DeleteContactResponse>>
    {
    }

    public class DeleteContactRequest
    {
        public int contacto_id { get; set; }
    }

    public class DeleteContactResponse
    {
        public contactoDto? contacto { get; set; }
    }
}
