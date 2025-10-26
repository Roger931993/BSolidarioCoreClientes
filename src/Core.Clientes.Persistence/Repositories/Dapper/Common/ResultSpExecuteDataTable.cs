using Core.Clientes.Domain.Interfaces.Dapper;
using Dapper;
using System.Data;

namespace Core.Clientes.Persistence.Repositories.Dapper.Common
{
    public class ResultSpExecuteDataTable : IResultSpExecuteDataTable
  {
    public DataTable ResultSp { get; private set; }
    public DynamicParameters Parameters { get; private set; }
    public void SetResultSp(DataTable value)
    {
      ResultSp = value;
    }

    public void SetParameters(DynamicParameters value)
    {
      Parameters = value;
    }
  }
}
