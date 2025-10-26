using Core.Clientes.Application.Interfaces.Persistence;
using Core.Clientes.Persistence.Contexts;
using Core.Clientes.Persistence.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Clientes.Persistence.Extensions
{
    public static class PersistenceServiceExtensions
    {
        public static IServiceCollection AddEFService(this IServiceCollection services, IConfiguration configuration)
        {
            #region SQL
            // Configura la conexión a la base de datos.
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Clientes"));
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Logs"));
            });
            #endregion       

            // Configura la conexión a la base de datos.
            services.AddScoped<IClienteRepository, ClienteRespository>();
            services.AddTransient<ILoggRepository, LoggRepository>();


            return services;
        }
    }
}
