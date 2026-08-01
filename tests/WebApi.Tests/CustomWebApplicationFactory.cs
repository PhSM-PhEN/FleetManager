using CommonTestUtilities.Entities;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Security.CryptoGraphy;
using FleetManager.Domain.Security.Token;
using FleetManager.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Tests.Resource;

namespace WebApi.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public UserIdentityManager USER_ADM { get; private set; } = default!;
        public UserIdentityManager USER_TEAM_MEMBER { get; private set;  } = default!;
        public AddressIdentityManager ADDRESS_TEAM_MEMBER { get ;  private set ;} = default!;
        public TenantIdentityManager TENANT_TEAM_MEMBER { get ; private set ;} = default!;
        public CompanyIdentityManager COMPANY_TEAM_MEMBER { get; private set; } = default!;
        public VehicleIdentityManager VEHICLE_TEAM_MEMBER { get; private set; } = default!;
        public RentalPlanIdentityManager RENTAL_PLAN_TEAM_MEMBER { get; private set; } = default!;
        public ContractIdentityManager CONTRACT_TEAM_MEMBER { get; private set; } = default!;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                    services.AddDbContext<FleetManagerDbContext>(config =>
                    {
                        config.UseInMemoryDatabase("InMemoryDbForTesting");
                        config.UseInternalServiceProvider(provider);
                    });
                    var scope = services.BuildServiceProvider().CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<FleetManagerDbContext>();
                    var passwordEncrypter = scope.ServiceProvider.GetRequiredService<IPasswordEncrypter>();
                    var accesTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                    StartDataBase(dbContext, passwordEncrypter, accesTokenGenerator);

                });
        }

        private void StartDataBase(FleetManagerDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator accesTokenGenerator)
        {
            AddUserAdmin(dbContext, passwordEncrypter, accesTokenGenerator);
            AddUserTeamMember(dbContext, passwordEncrypter, accesTokenGenerator);
            dbContext.SaveChanges();

            var address = AddAddress(dbContext);
            dbContext.SaveChanges();
            AddTenant(dbContext, address.Id);
            var company = AddCompany(dbContext, address.Id);
            dbContext.SaveChanges();
            
            var rentalPlan = AddRentalPlan(dbContext);

            var vehicle = AddVehicle(dbContext, company.Id, rentalPlan.Id);
            dbContext.SaveChanges();

            // Veiculo dedicado ao contrato semeado: o veiculo exposto em VEHICLE_TEAM_MEMBER
            // precisa continuar livre (sem contrato ativo) para os testes de registro de contrato.
            var contractVehicle = VehicleBuilder.Build(2, company.Id, rentalPlan.Id);
            dbContext.Add(contractVehicle);
            dbContext.SaveChanges();

            AddContract(dbContext, contractVehicle.Id, TENANT_TEAM_MEMBER.GetById(), rentalPlan);
            dbContext.SaveChanges();

        }
        private Contract AddContract(FleetManagerDbContext dbContext, long vehicleId, long tenantId, RentalPlan rentalPlan)
        {
            var contract = ContractBuilder.Build(vehicleId: vehicleId, tenantId: tenantId, rentalPlan: rentalPlan,
                status: FleetManager.Domain.Enum.ContractStatus.Reserved);
            dbContext.Contracts.Add(contract);
            CONTRACT_TEAM_MEMBER = new ContractIdentityManager(contract);
            return contract;
        }
        private RentalPlan  AddRentalPlan(FleetManagerDbContext dbContext)
        {
            var rentalPlan = RentalPlanBuilder.Build();
            dbContext.Add(rentalPlan);
            RENTAL_PLAN_TEAM_MEMBER = new RentalPlanIdentityManager(rentalPlan);
            return rentalPlan;
        }
        private Vehicle AddVehicle(FleetManagerDbContext dbContext, long companyId, long rentalPlanId)
        {
            var vehicle = VehicleBuilder.Build(1 , companyId, rentalPlanId);
            dbContext.Add(vehicle);
            VEHICLE_TEAM_MEMBER = new VehicleIdentityManager(vehicle);
            return vehicle;
        }
        private Company AddCompany(FleetManagerDbContext dbContext, long addressId)
        {
            var company = CompanyBuilder.Build(1, addressId);
            dbContext.Companys.Add(company);
            COMPANY_TEAM_MEMBER = new CompanyIdentityManager(company);
            return company;
        }
        private Tenant AddTenant(FleetManagerDbContext dbContext, long addressId)
        {
            var tenant = TenantBuilder.Build(1, addressId: addressId);
            dbContext.Tenants.Add(tenant);
            TENANT_TEAM_MEMBER = new TenantIdentityManager(tenant);
            return tenant;
        }
        private Address AddAddress(FleetManagerDbContext dbContext)
        {
            var address = AddressBuilder.Build(1);
            dbContext.Addresses.Add(address);

            ADDRESS_TEAM_MEMBER = new AddressIdentityManager(address);
            return address;
        }

        private User AddUserTeamMember(FleetManagerDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator accessTokenGenerator)
        {
            var user = UserBuilder.Build();
            var password = user.Password;
            user.ChangePassword(passwordEncrypter.Encrypt(password));
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var token = accessTokenGenerator.GenerateToken(user);
            USER_TEAM_MEMBER = new UserIdentityManager(user, password, token);

            return user;
        }


        private User AddUserAdmin(FleetManagerDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenGenerator)
        {
            var user = UserBuilder.Build();
            var password = user.Password;
            user.ChangePassword(passwordEncrypter.Encrypt(password));
            user.PromoteToAdmin();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var token = tokenGenerator.GenerateToken(user);
            USER_ADM = new UserIdentityManager(user, password, token);

            return user;
        }

    }
}
