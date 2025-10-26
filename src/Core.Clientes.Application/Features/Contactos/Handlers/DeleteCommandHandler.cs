using AutoMapper;
using Core.Clientes.Application.DTOs;
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
    public class DeleteCommandHandler : BaseCommand, IDecoradorRequestHandler<DeleteCommand, ResponseBase<DeleteContactResponse>>
  {
    private readonly IClienteRepository _customerRepository;

    public DeleteCommandHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, IClienteRepository customerRepository) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
    {
      this._customerRepository = customerRepository;
    }

    public async Task<ResponseBase<DeleteContactResponse>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
      DeleteContactRequest RequestData = request.Request!;
      Guid IdTraking = (Guid)request.IdTraking!;
      DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
      DeleteContactResponse objResponse = new DeleteContactResponse();
      try
      {
        contacto objSaved = _customerRepository.GetIncludesAsNoTraking<contacto>().FirstOrDefault(x => x.contacto_id == RequestData.contacto_id)!;
        if (objSaved != null)
          await _customerRepository.DeleteLogic<contacto>(RequestData.contacto_id)!;
        objResponse.contacto = _mapper.Map<contactoDto>(objSaved);
      }
      catch (Exception ex)
      {
        await AddLogError(RequestData, 500, ex, cachelocal);
        return await ErrorResponseEx<DeleteContactResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
      }
      return await OkResponse(objResponse);
    }
  }
}
