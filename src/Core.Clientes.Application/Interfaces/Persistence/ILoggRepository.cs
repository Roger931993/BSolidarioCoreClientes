using Core.Clientes.Domain.Entities;
using Core.Clientes.Domain.Models;

namespace Core.Clientes.Application.Interfaces.Persistence
{
    public interface ILoggRepository
    {      
        Task<api_log_cliente_header> SaveHeader(LoggingMdl model);
        Task<List<api_log_cliente_detail>> SaveDetails(List<api_log_cliente_detail> model);
    }
}
