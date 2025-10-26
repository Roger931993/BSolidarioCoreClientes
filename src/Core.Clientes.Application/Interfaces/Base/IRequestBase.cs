using Core.Clientes.Application.DTOs.Base;

namespace Core.Clientes.Application.Interfaces.Base
{
    public interface IRequestBase
    {
        Guid? IdTraking { get; set; }
        InfoSessionDto? InfoSession { get; set; }
    }
}
