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
    public class DeleteClienteCommandHandler : BaseCommand, IDecoradorRequestHandler<DeleteClienteCommand, ResponseBase<DeleteClienteResponse>>
  {
    private readonly IClienteRepository _customerRepository;

    public DeleteClienteCommandHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, IClienteRepository customerRepository) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
    {
      this._customerRepository = customerRepository;
    }

    public async Task<ResponseBase<DeleteClienteResponse>> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
    {
      DeleteClienteRequest RequestData = request.Request!;
      Guid IdTraking = (Guid)request.IdTraking!;
      DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
      DeleteClienteResponse objResponse = new DeleteClienteResponse();
      try
      {
        cliente objSaved = _customerRepository.GetIncludesAsNoTraking<cliente>().FirstOrDefault(x => x.cliente_id == RequestData.cliente_id)!;
        if (objSaved != null)
          await _customerRepository.DeleteLogic<cliente>(RequestData.cliente_id)!;
        objResponse.cliente = _mapper.Map<clienteDto>(objSaved);
      }
      catch (Exception ex)
      {
        await AddLogError(RequestData, 500, ex, cachelocal);
        return await ErrorResponseEx<DeleteClienteResponse>(IdTraking, ex, (int)TypeError.InternalError, Status: 500);
      }
      return await OkResponse(objResponse);
    }
  }
}
