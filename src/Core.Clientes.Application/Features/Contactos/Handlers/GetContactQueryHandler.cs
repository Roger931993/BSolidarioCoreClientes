using AutoMapper;
using Core.Clientes.Application.DTOs;
using Core.Clientes.Application.DTOs.Base;
using Core.Clientes.Application.Features.Contact.Queries;
using Core.Clientes.Application.Interfaces.Base;
using Core.Clientes.Application.Interfaces.Infraestructure;
using Core.Clientes.Application.Interfaces.Persistence;
using Core.Clientes.Domain.Common;
using Core.Clientes.Domain.Entities;
using static Core.Clientes.Model.Entity.EnumTypes;

namespace Core.Clientes.Application.Features.Contact.Handlers
{
    internal class GetContactQueryHandler : BaseCommand, IDecoradorRequestHandler<GetContactQuery, ResponseBase<GetContactResponse>>
    {
        private readonly IClienteRepository _customerRepository;

        public GetContactQueryHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, IClienteRepository customerRepository) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._customerRepository = customerRepository;
        }

        public async Task<ResponseBase<GetContactResponse>> Handle(GetContactQuery request, CancellationToken cancellationToken)
        {
            GetContactRequest RequestData = request.request.Request!;

            Guid IdTraking = (Guid)request.request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            GetContactResponse objResponse = new GetContactResponse();

            try
            {
                List<contactoDto> objResult = new List<contactoDto>();
                if (RequestData.cliente_id != null)
                {
                    List<contacto> objSaved = _customerRepository.GetIncludesAsNoTraking<contacto>(x=>x.cliente!).Where(x => x.cliente_id == RequestData.cliente_id && x.estado != (int)TypeStatus.Disable)!.ToList();
                    objResponse.contacto = _mapper.Map<List<contactoDto>>(objSaved);
                }
                if (RequestData.contacto_id != null)
                {
                    List<contacto> objSaved = _customerRepository.GetIncludesAsNoTraking<contacto>(x => x.cliente!).Where(x => x.cliente_id == RequestData.contacto_id && x.estado != (int)TypeStatus.Disable)!.ToList();
                    objResponse.contacto = _mapper.Map<List<contactoDto>>(objSaved);
                }
                if (RequestData.cliente_id == null
                    && RequestData.contacto_id == null)
                {
                    List<contacto> objSaved = _customerRepository.GetIncludesAsNoTraking<contacto>(x => x.cliente!).Where(x => x.estado != (int)TypeStatus.Disable)!.ToList();                    
                    objResponse.contacto = _mapper.Map<List<contactoDto>>(objSaved);
                }
                
            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<GetContactResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
