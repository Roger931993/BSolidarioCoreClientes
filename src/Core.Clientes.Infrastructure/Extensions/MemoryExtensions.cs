using Core.Clientes.Application.Interfaces.Infraestructure;
using Core.Clientes.Infrastructure.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Clientes.Infrastructure.Extensions
{
    public static class MemoryExtensions
    {
        public static IServiceCollection AddMemoryCache(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar MemoryCache
            services.AddMemoryCache();
            // Registrar tus servicios
            services.AddSingleton<IMemoryCacheLocalService, MemoryCacheLocalService>();

            return services;
        }
    }
}
