using Core.Clientes.Domain.Common.Dapper;

namespace Core.Clientes.Domain.Interfaces.Dapper
{
    public interface IRepositoryExecute<TDomainEntity> where TDomainEntity : EntityExecuteText
  {
    Task<int> ExecuteQueryIntReturn(TDomainEntity entity, int? commandTimeout = null, object param = null);
  }
}
