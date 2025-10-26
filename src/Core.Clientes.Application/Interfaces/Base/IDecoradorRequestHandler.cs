using MediatR;

namespace Core.Clientes.Application.Interfaces.Base
{
    public interface IDecoradorRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
    }
}
