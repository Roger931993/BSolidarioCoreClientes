using Core.Clientes.Domain.Interfaces.Dapper;
using Dapper;

namespace Core.Clientes.Persistence.Repositories.Dapper.Common
{
    public class ResultSpExecuteGridReader : IResultSpExecuteGridReader
  {
    public SqlMapper.GridReader ResultSp { get; private set; }
    public DynamicParameters Parameters { get; private set; }
    public void SetResultSp(SqlMapper.GridReader value)
    {
      ResultSp = value;
    }

    public void SetParameters(DynamicParameters value)
    {
      Parameters = value;
    }
  }
}
