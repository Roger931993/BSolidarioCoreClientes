using Core.Clientes.Application.Interfaces.Persistence;
using Core.Clientes.Domain.Interfaces.Dapper;
using Core.Clientes.Persistence.Contexts;
using Core.Clientes.Persistence.Repositories.Dapper.Common;

namespace Core.Clientes.Persistence.Repositories.Dapper
{
    public class UnitOfWorkStamp : UnitOfWork, IUnitOfWorkStamp
    {
        private readonly IDatabaseConnect _options;
        public UnitOfWorkStamp(ClientesContextCommand contextDapper, IDatabaseConnect options) : base(contextDapper)
        {
            _options = options;
        }
    }
}
