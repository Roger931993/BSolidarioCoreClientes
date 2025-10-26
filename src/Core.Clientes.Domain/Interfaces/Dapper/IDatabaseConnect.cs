using System.Data;

namespace Core.Clientes.Domain.Interfaces.Dapper
{
  public interface IDatabaseConnect
  {
    IDbConnection GetConnection(string coonectionName);
  }
}
