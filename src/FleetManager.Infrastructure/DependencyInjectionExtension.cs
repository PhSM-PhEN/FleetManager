using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Domain.Repositories.ToCharge;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Domain.Repositories.ToContractDocument;
using FleetManager.Domain.Repositories.ToContractTemplate;
using FleetManager.Domain.Repositories.ToIncidentReport;
using FleetManager.Domain.Repositories.ToRentalPlan;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Domain.Security.CryptoGraphy;
using FleetManager.Domain.Security.Token;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Exception.ExceptionBase;
using FleetManager.Infrastructure.DataAccess;
using FleetManager.Infrastructure.DataAccess.ToAddress;
using FleetManager.Infrastructure.DataAccess.ToCharge;
using FleetManager.Infrastructure.DataAccess.ToCompany;
using FleetManager.Infrastructure.DataAccess.ToContract;
using FleetManager.Infrastructure.DataAccess.ToContractDocument;
using FleetManager.Infrastructure.DataAccess.ToContractTemplate;
using FleetManager.Infrastructure.DataAccess.ToIncidentReport;
using FleetManager.Infrastructure.DataAccess.ToRentalPlan;
using FleetManager.Infrastructure.DataAccess.ToTenant;
using FleetManager.Infrastructure.DataAccess.ToUser;
using FleetManager.Infrastructure.DataAccess.ToVehicle;
using FleetManager.Infrastructure.Extension;
using FleetManager.Infrastructure.Security.Token;
using FleetManager.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FleetManager.Infrastructure.DataAccess.ToMaintenance;
using FleetManager.Domain.Repositories.ToMaintenance;

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

            if (configuration.IsTestEnvironment() == false)
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
            // users
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();

            //address
            services.AddScoped<IAddressReadOnlyRepository, AddressRepository>();
            services.AddScoped<IAddressWriteOnlyRepository, AddressRepository>();
            // tenant
            services.AddScoped<ITenantWriteOnlyRepository, TenantRepository>();
            services.AddScoped<ITenantReadOnlyRepository, TenantRepository>();
            // company
            services.AddScoped<ICompanyReadOnlyRepository, CompanyRepository>();
            services.AddScoped<ICompanyWriteOnlyRepository, CompanyRepository>();
            //vehicle
            services.AddScoped<IVehicleReadOnlyRepository, VehicleRepository>();
            services.AddScoped<IVehicleWriteOnlyRepository, VehicleRepository>();
            //rental plan
            services.AddScoped<IRentalPlanReadOnlyRepository, RentalPlanRepository>();
            services.AddScoped<IRentalPlanWriteOnlyRepository, RentalPlanRepository>();
            //contract
            services.AddScoped<IContractReadOnlyRepository, ContractRepository>();
            services.AddScoped<IContractWriteOnlyRepository, ContractRepository>();
            //incident report
            services.AddScoped<IIncidentReportReadOnlyRepository, IncidentReportRepository>();
            services.AddScoped<IIncidentReportWriteOnlyRepository, IncidentReportRepository>();
            //charge
            services.AddScoped<IChargeReadOnlyRepository, ChargeRepository>();
            services.AddScoped<IChargeWriteOnlyRepository, ChargeRepository>();
            // maintenance
            services.AddScoped<IMaintenanceReadOnlyRepository, MaintenanceRepository>();   
            services.AddScoped<IMaintenanceWriteOnlyRepository, MaintenanceRepository>();

            services.AddScoped<IContractTemplateReadOnlyRepository, ContractTemplateRepository>();
            services.AddScoped<IContractTemplateWriteOnlyRepository, ContractTemplateRepository>();
            services.AddScoped<IContractDocumentWriteOnlyRepository, ContractDocumentRepository>();
            // unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
