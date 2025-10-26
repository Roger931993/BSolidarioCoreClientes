using Core.Clientes.Domain.Common.Dapper;

namespace Core.Clientes.Domain.Interfaces.Dapper
{
    public interface IRepositoryProcedures<TDomainEntity> where TDomainEntity : EntitySp
  {
    Task<TDomainEntity> ExecuteSpAsync(TDomainEntity entity);
    Task<IResponseDataSetSpExecution<TDomainEntity>> ExecuteSpAsyncDataSetReturn(TDomainEntity entity);
    Task<IResponseDataSetSpExecution<TDomainEntity>> ExecuteSpAsyncDataTableReturn(TDomainEntity entity);
    Task<IResponseGridReaderSpExecution<TDomainEntity>> ExecuteSpAsyncGridReaderReturn(TDomainEntity entity);
    Task<IResponseGridReaderSpExecution<TDomainEntity>> ExecuteFunctionAsyncGridReaderReturn(TDomainEntity entity);

  }
}
