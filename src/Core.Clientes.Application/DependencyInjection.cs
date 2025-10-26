using Core.Clientes.Application.Common;
using Core.Clientes.Application.Interfaces;
using Core.Clientes.Application.Interfaces.Base;
using Core.Clientes.Application.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Clientes.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ApplicationMappingProfile));

            services.AddScoped<IErrorCatalogException, ErrorCatalogException>();

            services.AddMediatR(gfc => gfc.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped<IPermissionService, PermissionService>();

            return services;
        }
    }
}
