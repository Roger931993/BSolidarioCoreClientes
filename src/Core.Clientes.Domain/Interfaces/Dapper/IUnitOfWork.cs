using Core.Clientes.Domain.Common.Dapper;

namespace Core.Clientes.Domain.Interfaces.Dapper
{
    public interface IUnitOfWork
  {
    IRepositoryCommand<TDomainEntity> RepositoryCommand<TDomainEntity>() where TDomainEntity : Entity;


    IRepositoryProcedures<TDomainEntity> RepositoryProcedure<TDomainEntity>() where TDomainEntity : EntitySp;
    void CommitTransaction();
    void RollBackTransaction();

    void BeginTransaccion();
  }
}
