using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Security.CryptoGraphy;
using FleetManager.Domain.Security.Token;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Exception.ExceptionBase;
using FleetManager.Infrastructure.DataAccess;
using FleetManager.Infrastructure.DataAccess.ToUser;
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
            services.AddScoped<IPasswordEncrypter, Security.Cryptography.BCrypt>();
            services.AddScoped<ILoggedUser, LoggedUser>();
            AddToken(services, configuration);
            AddRepositories(services);

            if (configuration.IsTestEnviromment() == false)
            {
                AddDataContext(services, configuration);
            }
        }
        private static void AddToken(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTime = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
            var sigingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey")
                ?? throw new InvalidOperationException(ResourceErrorMessages.JWT_NOT_CONFIGURED);

            var issuer = configuration.GetValue<string>("Settings:Jwt:Issuer") ?? "FleetManagerApi";
            var audience = configuration.GetValue<string>("Settings:Jwt:Audience") ?? "FleetManagerClients";

            services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTime, sigingKey, issuer, audience));
        }
        private static void AddDataContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<FleetManagerDbContext>(options =>
            {
                options.UseMySql(connectionString, serverVersion);
            });
        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();


            // unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
