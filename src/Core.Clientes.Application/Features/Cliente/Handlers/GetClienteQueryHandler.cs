using AutoMapper;
using Core.Clientes.Application.DTOs;
using Core.Clientes.Application.DTOs.Base;
using Core.Clientes.Application.Features.Cliente.Queries;
using Core.Clientes.Application.Interfaces.Base;
using Core.Clientes.Application.Interfaces.Infraestructure;
using Core.Clientes.Application.Interfaces.Persistence;
using Core.Clientes.Domain.Common;
using Core.Clientes.Domain.Entities;
using static Core.Clientes.Model.Entity.EnumTypes;

namespace Core.Clientes.Application.Features.Cliente.Handlers
{
    internal class GetClienteQueryHandler : BaseCommand, IDecoradorRequestHandler<GetClienteQuery, ResponseBase<GetClienteResponse>>
    {
        private readonly IClienteRepository _customerRepository;

        public GetClienteQueryHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, IClienteRepository customerRepository) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._customerRepository = customerRepository;
        }

        public async Task<ResponseBase<GetClienteResponse>> Handle(GetClienteQuery request, CancellationToken cancellationToken)
        {
            GetClienteRequest RequestData = request.request.Request!;

            Guid IdTraking = (Guid)request.request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            GetClienteResponse objResponse = new GetClienteResponse();

            try
            {
                List<clienteDto> objResult = new List<clienteDto>();
                if (RequestData.TypeGetCliente == TypeGetCliente.ById)
                {
                    List<cliente> objSaved = _customerRepository.GetIncludesAsNoTraking<cliente>(x => x.contacto!).Where(x => x.cliente_id == RequestData.cliente_id)!.ToList();
                    objResponse.cliente = _mapper.Map<List<clienteDto>>(objSaved);
                }
                if (RequestData.TypeGetCliente == TypeGetCliente.None)
                {
                    List<cliente> objSaved = _customerRepository.GetIncludesAsNoTraking<cliente>(x => x.contacto!).ToList();                    
                    objResponse.cliente = _mapper.Map<List<clienteDto>>(objSaved);
                }
                if (RequestData.TypeGetCliente == TypeGetCliente.ByIdentificacion)
                {
                    List<cliente> objSaved = _customerRepository.GetIncludesAsNoTraking<cliente>(x => x.contacto!).Where(x => x.identificacion == RequestData.identificacion)!.ToList();
                    objResponse.cliente = _mapper.Map<List<clienteDto>>(objSaved);
                }
                if (RequestData.TypeGetCliente == TypeGetCliente.ByUserName)
                {
                    List<cliente> objSaved = _customerRepository.GetIncludesAsNoTraking<cliente>(x => x.contacto!).Where(x => x.username == RequestData.username)!.ToList();
                    objResponse.cliente = _mapper.Map<List<clienteDto>>(objSaved);
                }

            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<GetClienteResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
