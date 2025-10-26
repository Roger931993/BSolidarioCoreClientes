using Core.Clientes.API.Filters;
using Core.Clientes.Application.DTOs.Base;
using Core.Clientes.Application.Features.Cliente.Commands;
using Core.Clientes.Application.Features.Cliente.Queries;
using Core.Clientes.Application.Interfaces.Infraestructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Core.Clientes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : CommonController
    {
        public ClientesController(IMediator mediator, IMemoryCacheLocalService memoryCacheLocalService, IRedisCache redisCache) : base(mediator, memoryCacheLocalService, redisCache)
        {
        }


        /// <summary>
        /// Obtener todos los clientes
        /// </summary>
        /// <remarks>
        /// Permiso: ClienteController-GetAllQuery
        /// <br/>
        /// Descripcion: Obtener todos los clientes
        /// </remarks>    
        [HttpGet()]        
        [Permission("ClienteController-GetAllQuery")]
        [ProducesResponseType(typeof(GetClienteResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetClienteResponse>> GetAllQuery()
        {
            RequestBase<GetClienteRequest> request = new RequestBase<GetClienteRequest>()
            {
                Request = new GetClienteRequest()
                {                    
                    TypeGetCliente = TypeGetCliente.None
                }
            };
            await CreateDataCacheLocal(HttpContext, request);
            ResponseBase<GetClienteResponse> objResponse = await _mediator.Send(new GetClienteQuery(request));
            return OkUrban(objResponse);
        }

        /// <summary>
        /// Obtener cliente por id
        /// </summary>
        /// <remarks>
        /// Permiso: ClienteController-GetById
        /// <br/>
        /// Descripcion: Obtener cliente por id
        /// </remarks>    
        [HttpGet("id/{id}")]        
        [Permission("ClienteController-GetById")]
        [ProducesResponseType(typeof(GetClienteResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetClienteResponse>> GetById(int id)
        {
            RequestBase<GetClienteRequest> request = new RequestBase<GetClienteRequest>()
            {
                Request = new GetClienteRequest()
                {
                    cliente_id = id,
                    TypeGetCliente = TypeGetCliente.ById
                }
            };
            await CreateDataCacheLocal(HttpContext, request);
            ResponseBase<GetClienteResponse> objResponse = await _mediator.Send(new GetClienteQuery(request));
            return OkUrban(objResponse);
        }

        /// <summary>
        /// Obtener cliente por id
        /// </summary>
        /// <remarks>
        /// Permiso: ClienteController-GetByIdentificacion
        /// <br/>
        /// Descripcion: Obtener cliente por id
        /// </remarks>    
        [HttpGet("identificacion/{id}")]
        [Permission("ClienteController-GetByIdentificacion")]
        [ProducesResponseType(typeof(GetClienteResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetClienteResponse>> GetByIdentificacion(int id)
        {
            RequestBase<GetClienteRequest> request = new RequestBase<GetClienteRequest>()
            {
                Request = new GetClienteRequest()
                {
                    cliente_id = id,
                    TypeGetCliente = TypeGetCliente.ByIdentificacion
                }
            };
            await CreateDataCacheLocal(HttpContext, request);
            ResponseBase<GetClienteResponse> objResponse = await _mediator.Send(new GetClienteQuery(request));
            return OkUrban(objResponse);
        }

        /// <summary>
        /// Obtener cliente por id
        /// </summary>
        /// <remarks>
        /// Permiso: ClienteController-GetByIdentificacion
        /// <br/>
        /// Descripcion: Obtener cliente por id
        /// </remarks>    
        [HttpGet("username/{id}")]
        [Permission("ClienteController-GetByUsername")]
        [ProducesResponseType(typeof(GetClienteResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetClienteResponse>> GetByUsername(string id)
        {
            RequestBase<GetClienteRequest> request = new RequestBase<GetClienteRequest>()
            {
                Request = new GetClienteRequest()
                {
                    username = id,
                    TypeGetCliente = TypeGetCliente.ByUserName
                }
            };
            await CreateDataCacheLocal(HttpContext, request);
            ResponseBase<GetClienteResponse> objResponse = await _mediator.Send(new GetClienteQuery(request));
            return OkUrban(objResponse);
        }

        /// <summary>
        /// Registrar cliente
        /// </summary>
        /// <remarks>
        /// Permiso: ClienteController-Register
        /// <br/>
        /// Descripcion: Registrar cliente
        /// </remarks>    
        [HttpPost("registrar")]
        [Authorize]
        [Permission("ClienteController-Register")]
        [ProducesResponseType(typeof(RegisterClienteResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RegisterClienteResponse>> Register([FromBody] RegisterClienteRequest data)
        {
            RegisterClienteCommand command = new RegisterClienteCommand()
            {
                Request = data
            };
            await CreateDataCacheLocal(HttpContext, command);
            ResponseBase<RegisterClienteResponse> objResponse = await _mediator.Send(command);
            return OkUrban(objResponse);
        }

        /// <summary>
        /// Eliminar cliente
        /// </summary>
        /// <remarks>
        /// Permiso: ClienteController-Delete
        /// <br/>
        /// Descripcion: Eliminar cliente
        /// </remarks>    
        [HttpDelete("{id}")]
        [Authorize]
        [Permission("ClienteController-Delete")]
        [ProducesResponseType(typeof(DeleteClienteResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DeleteClienteResponse>> Delete(int id)
        {
            DeleteClienteCommand command = new DeleteClienteCommand()
            {
                Request = new DeleteClienteRequest()
                {
                    cliente_id = id
                }
            };
            await CreateDataCacheLocal(HttpContext, command);
            ResponseBase<DeleteClienteResponse> objResponse = await _mediator.Send(command);
            return OkUrban(objResponse);
        }
    }
}
