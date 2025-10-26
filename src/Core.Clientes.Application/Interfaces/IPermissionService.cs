namespace Core.Clientes.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(string idSession, string Verb, string permission);
    }
}
