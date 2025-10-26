using Core.Clientes.API.Filters;
using Core.Clientes.Application.DTOs.Base;
using Core.Clientes.Application.Features.Contact.Commands;
using Core.Clientes.Application.Features.Contact.Queries;
using Core.Clientes.Application.Interfaces.Infraestructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Core.Clientes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactosController : CommonController
    {
        public ContactosController(IMediator mediator, IMemoryCacheLocalService memoryCacheLocalService, IRedisCache redisCache) : base(mediator, memoryCacheLocalService, redisCache)
        {
        }

        /// <summary>
        /// Obtener todas los contactos
        /// </summary>
        /// <remarks>
        /// Permiso: ContactosController-GetAllQuery
        /// <br/>
        /// Descripcion: Obtener todas los contactos
        /// </remarks>    
        [HttpGet()]        
        [Permission("ContactosController-GetAllQuery")]
        [ProducesResponseType(typeof(GetContactResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetContactResponse>> GetAllQuery()
        {
            RequestBase<GetContactRequest> request = new RequestBase<GetContactRequest>()
            {
                Request = new GetContactRequest()
                {                    
                }
            };
            await CreateDataCacheLocal(HttpContext, request);
            ResponseBase<GetContactResponse> objResponse = await _mediator.Send(new GetContactQuery(request));
            return OkUrban(objResponse);
        }


        /// <summary>
        /// Obtener contacto por id
        /// </summary>
        /// <remarks>
        /// Permiso: ContactosController-GetById
        /// <br/>
        /// Descripcion: Obtener contacto por id
        /// </remarks>    
        [HttpGet("id/{id}")]        
        [Permission("ContactosController-GetById")]
        [ProducesResponseType(typeof(GetContactResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetContactResponse>> GetByIdQuery(int id)
        {
            RequestBase<GetContactRequest> request = new RequestBase<GetContactRequest>()
            {
                Request = new GetContactRequest()
                {
                    contacto_id = id
                }
            };
            await CreateDataCacheLocal(HttpContext, request);
            ResponseBase<GetContactResponse> objResponse = await _mediator.Send(new GetContactQuery(request));
            return OkUrban(objResponse);
        }

        /// <summary>
        /// Obtener contacto por id de cliente
        /// </summary>
        /// <remarks>
        /// Permiso: ContactosController-GetByCliente
        /// <br/>
        /// Descripcion: Obtener contacto por id de cliente
        /// </remarks>    
        [HttpGet("cliente/{id}")]        
        [Permission("ContactosController-GetByCliente")]
        [ProducesResponseType(typeof(GetContactResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetContactResponse>> GetByClienteQuery(int id)
        {
            RequestBase<GetContactRequest> request = new RequestBase<GetContactRequest>()
            {
                Request = new GetContactRequest()
                {
                    cliente_id = id
                }
            };
            await CreateDataCacheLocal(HttpContext, request);
            ResponseBase<GetContactResponse> objResponse = await _mediator.Send(new GetContactQuery(request));
            return OkUrban(objResponse);
        }

        /// <summary>
        /// Registrar contacto
        /// </summary>
        /// <remarks>
        /// Permiso: ContactosController-Register
        /// <br/>
        /// Descripcion: Registrar contacto
        /// </remarks>  
        [HttpPost("registrar")]
        [Authorize]
        [Permission("ContactosController-Register")]
        [ProducesResponseType(typeof(RegisterContactResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RegisterContactResponse>> Register([FromBody] RegisterContactRequest data)
        {
            RegisterCommand command = new RegisterCommand()
            {
                Request = data
            };
            await CreateDataCacheLocal(HttpContext, command);
            ResponseBase<RegisterContactResponse> objResponse = await _mediator.Send(command);
            return OkUrban(objResponse);
        }

        /// <summary>
        /// Eliminar contacto
        /// </summary>
        /// <remarks>
        /// Permiso: ContactosController-Delete
        /// <br/>
        /// Descripcion: Eliminar contacto
        /// </remarks>  
        [HttpDelete("{id}")]
        [Authorize]
        [Permission("ContactosController-Delete")]
        [ProducesResponseType(typeof(DeleteContactResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DeleteContactResponse>> Delete(int id)
        {
            DeleteCommand command = new DeleteCommand()
            {
                Request = new DeleteContactRequest()
                {
                    contacto_id = id
                }
            };
            await CreateDataCacheLocal(HttpContext, command);
            ResponseBase<DeleteContactResponse> objResponse = await _mediator.Send(command);
            return OkUrban(objResponse);
        }
    }
}
