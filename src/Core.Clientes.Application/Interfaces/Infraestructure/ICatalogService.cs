using Core.Clientes.Application.DTOs.Base;
using Core.Clientes.Application.DTOs.Catalog;

namespace Core.Clientes.Application.Interfaces.Infraestructure
{
    public interface ICatalogService
    {
        Task<ResponseBase<GetErrorByIdResponse>> GetCatalogErrorById(int id, Guid IdTraker);
    }
}
