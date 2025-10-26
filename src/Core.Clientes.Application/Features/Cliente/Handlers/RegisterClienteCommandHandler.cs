using AutoMapper;
using Core.Clientes.Application.DTOs;
using Core.Clientes.Application.DTOs.Base;
using Core.Clientes.Application.Features.Cliente.Commands;
using Core.Clientes.Application.Interfaces.Base;
using Core.Clientes.Application.Interfaces.Infraestructure;
using Core.Clientes.Application.Interfaces.Persistence;
using Core.Clientes.Domain.Common;
using Core.Clientes.Domain.Entities;
using static Core.Clientes.Model.Entity.EnumTypes;

namespace Core.Clientes.Application.Features.Cliente.Handlers
{
    public class RegisterClienteCommandHandler : BaseCommand, IDecoradorRequestHandler<RegisterClienteCommand, ResponseBase<RegisterClienteResponse>>
    {
        private readonly IClienteRepository _customerRepository;

        public RegisterClienteCommandHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, IClienteRepository customerRepository) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._customerRepository = customerRepository;
        }

        public async Task<ResponseBase<RegisterClienteResponse>> Handle(RegisterClienteCommand request, CancellationToken cancellationToken)
        {
            RegisterClienteRequest RequestData = request.Request!;
            Guid IdTraking = (Guid)request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            RegisterClienteResponse objResponse = new RegisterClienteResponse();
            try
            {
                cliente objSaved = _customerRepository.GetIncludesAsNoTraking<cliente>().FirstOrDefault(x => x.cliente_id == RequestData.cliente_id)!;
                if (objSaved == null)
                {
                    cliente objNew = new cliente()
                    {
                        apellido_materno = RequestData.apellido_materno,
                        apellido_paterno = RequestData.apellido_paterno,
                        identificacion = RequestData.identificacion,
                        primer_nombre = RequestData.primer_nombre,
                        segundo_nombre = RequestData.segundo_nombre, 
                        username = RequestData.username,
                        estado = (int)TypeStatus.Active,
                    };
                    objNew = await _customerRepository.Save(objNew);
                    objResponse.cliente = _mapper.Map<clienteDto>(objNew);
                }
                else
                {
                    //List<string> camposForzarModificacion = new List<string>();
                    //camposForzarModificacion.Add("strategy_framework_country_id");
                    //camposForzarModificacion.Add("value");

                    objSaved.apellido_materno = RequestData.apellido_materno;
                    objSaved.apellido_paterno = RequestData.apellido_paterno;
                    objSaved.identificacion = RequestData.identificacion;
                    objSaved.primer_nombre = RequestData.primer_nombre;
                    objSaved.segundo_nombre = RequestData.segundo_nombre;   
                    objSaved.username = RequestData.username;   
                    objSaved.estado = RequestData.estado;

                    objSaved = await _customerRepository.Update(objSaved.cliente_id, objSaved);
                    objResponse.cliente = _mapper.Map<clienteDto>(objSaved);
                }
            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<RegisterClienteResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
