using Dapper;

namespace Core.Clientes.Domain.Interfaces.Dapper
{
  public interface IResultSpExecuteGridReader
  {
    SqlMapper.GridReader ResultSp { get; }
    DynamicParameters Parameters { get; }
    void SetResultSp(SqlMapper.GridReader value);
    void SetParameters(DynamicParameters value);
  }
}
