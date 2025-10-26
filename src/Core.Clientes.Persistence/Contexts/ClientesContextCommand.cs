using Core.Clientes.Domain.Interfaces.Dapper;
using Core.Clientes.Persistence.Repositories.Dapper.Common;

namespace Core.Clientes.Persistence.Contexts
{
    public class ClientesContextCommand : DbContextDapperCommon
    {
        public ClientesContextCommand(IDatabaseConnect options) : base(options.GetConnection("Clientes"))
        {
        }
    }
}
