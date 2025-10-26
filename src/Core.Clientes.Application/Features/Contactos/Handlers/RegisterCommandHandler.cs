using AutoMapper;
using Core.Clientes.Application.DTOs.Base;
using Core.Clientes.Application.Features.Contact.Commands;
using Core.Clientes.Application.Interfaces.Base;
using Core.Clientes.Application.Interfaces.Infraestructure;
using Core.Clientes.Application.Interfaces.Persistence;
using Core.Clientes.Domain.Common;
using Core.Clientes.Domain.Entities;
using static Core.Clientes.Model.Entity.EnumTypes;

namespace Core.Clientes.Application.Features.Contact.Handlers
{
    public class RegisterCommandHandler : BaseCommand, IDecoradorRequestHandler<RegisterCommand, ResponseBase<RegisterContactResponse>>
    {
        private readonly IClienteRepository _customerRepository;

        public RegisterCommandHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, IClienteRepository customerRepository) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._customerRepository = customerRepository;
        }

        public async Task<ResponseBase<RegisterContactResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            RegisterContactRequest RequestData = request.Request!;
            Guid IdTraking = (Guid)request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            RegisterContactResponse objResponse = new RegisterContactResponse();
            try
            {
                cliente customer = _customerRepository.GetIncludesAsNoTraking<cliente>().FirstOrDefault(x => x.cliente_id == RequestData.cliente_id && x.estado != (int)TypeStatus.Disable)!;
                if (customer == null)
                    return await ErrorResponse<RegisterContactResponse>(IdTraking, (int)TypeError.NoInformation);
                contacto objSaved = _customerRepository.GetIncludesAsNoTraking<contacto>().FirstOrDefault(x => x.contacto_id == RequestData.contacto_id)!;
                if (objSaved == null)
                {
                    contacto objNew = new contacto()
                    {
                        cliente_id = RequestData.cliente_id,
                        descripcion = RequestData.descripcion,
                        email = RequestData.email,
                        telefono = RequestData.telefono,                        
                        estado = (int)TypeStatus.Active,
                    };
                    objNew = await _customerRepository.Save(objNew);
                    objResponse = _mapper.Map<RegisterContactResponse>(objNew);
                }
                else
                {                   
                    objSaved.cliente_id = RequestData.cliente_id;
                    objSaved.descripcion = RequestData.descripcion;
                    objSaved.email = RequestData.email;
                    objSaved.telefono = RequestData.telefono;
                    objSaved.estado = (int)TypeStatus.Active;

                    objSaved = await _customerRepository.Update(objSaved.contacto_id, objSaved);
                    objResponse = _mapper.Map<RegisterContactResponse>(objSaved);
                }
            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<RegisterContactResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
