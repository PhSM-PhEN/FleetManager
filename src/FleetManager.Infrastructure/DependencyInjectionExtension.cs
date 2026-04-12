using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Infrastructure.DataAccess;
using FleetManager.Infrastructure.DataAccess.ToCategory;
using FleetManager.Infrastructure.DataAccess.ToVehicle;
using FleetManager.Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManager.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            if (configuration.IsTestEnviroment() == false)
            {
                AddDataContext(services, configuration);
                
            }
        }

        private static void AddDataContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectString = configuration.GetConnectionString("Connection");
            var serverVersion =  ServerVersion.AutoDetect(connectString);

            services.AddDbContext<FleetManagerDbContext>(config => config.UseMySql(connectString, serverVersion));

        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICategoryWriteOnlyRepository, CategoryRepository>();
            services.AddScoped<ICategoryReadOnlyRepository, CategoryRepository>();
            services.AddScoped<ICategoryUpdateOnlyRepository, CategoryRepository>();

            services.AddScoped<IVehicleWriteOnlyRepository, VehicleRepository>();
            services.AddScoped<IVehicleReadOnlyRepository, VehicleRepository>();
            services.AddScoped<IVehicleUpdateOnlyRepository, VehicleRepository>();

            services.AddScoped<IUnitOfWork, UnitiOkWork>();
        }

    }
}
