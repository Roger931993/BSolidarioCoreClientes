using Core.Clientes.Domain.Common.Dapper;
using Dapper;

namespace Core.Clientes.Domain.Interfaces.Dapper
{
    public interface IResponseGridReaderSpExecution<TDomainResponse> where TDomainResponse : EntitySp
  {
    TDomainResponse EntityDomainResponse { get; }
    SqlMapper.GridReader GridReaderResult { get; }
    void SetGridReader(SqlMapper.GridReader dataset);
    void SetEntity(TDomainResponse entity);
  }
}
