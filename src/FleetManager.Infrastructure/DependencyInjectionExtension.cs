using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Domain.Security.Cryptography;
using FleetManager.Domain.Security.Token;
using FleetManager.Domain.Services.LoggeUser;
using FleetManager.Infrastructure.DataAccess;
using FleetManager.Infrastructure.DataAccess.ToCategory;
using FleetManager.Infrastructure.DataAccess.ToUsers;
using FleetManager.Infrastructure.DataAccess.ToVehicle;
using FleetManager.Infrastructure.Extension;
using FleetManager.Infrastructure.Security.Token;
using FleetManager.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManager.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
            services.AddScoped<ILoggedUser, LoggedUser>();
            AddToken(services, configuration);
            AddRepositories(services);

            if (configuration.IsTestEnviroment() == false)
            {
                AddDataContext(services, configuration);
                
            }
        }
        private static void AddToken(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresInMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAccesTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
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

            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();

            

            services.AddScoped<IUnitOfWork, UnitiOkWork>();
        }

    }
}
