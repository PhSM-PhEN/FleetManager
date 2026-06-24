using CommonTestUtilities.Entitie;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Security.Cryptography;
using FleetManager.Domain.Security.Token;
using FleetManager.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Tests.Resource;



namespace WebApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{ 
    public AddressIdentityManager ADDRESS_TEAM_MEMBER {get ; private set ;} = default!;
    public AddressIdentityManager ADDRESS_ADM_MEMBER {get ; private set ;} = default!;
    public CategoryIdentitiyManager CATEGORY_TEAM_MEMBER {get ; private set ;} = default!;
    public CategoryIdentitiyManager CATEGORY_ADM_MEMBER {get ; private set ;} = default!;
    public ClientIdentityManager CLIENT_TEAM_MEMBER {get ; private set ;} = default!;
    public CompanyIdentityManager COMPANY_TEAM_MEMBER { get; private set; } = default!;
    public CompanyIdentityManager COMPANY_ADM_MEMBER { get; private set; } = default!;
    public RentalPlanIdentityManager RENTAL_PLAN_TEAM_MEMBER {get ; private set ;} = default!;
    public RentalPlanIdentityManager RENTAL_PLAN_ADM_MEMBER {get ; private set ;} = default!;
    public RentalIdentityManager RENTAL_TEAM_MEMBER { get; private set; } = default!;
    public RentalIdentityManager RENTAL_ADM_MEMBER { get; private set; } = default!;
    public UserIdentityManager USER_TEAM_MEMBER { get; private set; } = default!;
    public UserIdentityManager USER_ADM_MEMBER { get; private set; } = default!;
    public VehicleIdentitiyManager VEHICLE_TEAM_MEMBER { get ; private set ;} = default!;
    public VehicleIdentitiyManager VEHICLE_ADM_MEMBER {get ; private set ;}  = default!;

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
                var passwordEncryter = scope.ServiceProvider.GetRequiredService<IPasswordEncrypter>();
                var accessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                StartDataBase(dbContext, passwordEncryter, accessTokenGenerator);

            });
    }
    private void StartDataBase(FleetManagerDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenGenerator)
    {
    
        var addressTeamMember = AddAddress(dbContext);
        var addressAdmMember = AddAddress(dbContext);
        dbContext.SaveChanges();

    
        var companyTeamMember = AddCompany(dbContext, addressTeamMember.Id);
        var companyAdmMember = AddCompany(dbContext, addressAdmMember.Id);

    
        var categoryTeamMember = AddCategory(dbContext);
        var categoryAdmMember = AddCategory(dbContext);
        dbContext.SaveChanges();

    
        var vehicleTeamMember = AddVehicle(dbContext, categoryTeamMember.Id);
        var vehicleAdmMember = AddVehicle(dbContext, categoryAdmMember.Id);

   
        var clientTeamMember = AddClient(dbContext, addressTeamMember.Id);
        var clientAdmMember = AddClient(dbContext, addressAdmMember.Id);

    
        AddUserTeamMember(dbContext, passwordEncrypter, tokenGenerator);
        AddUserAdmin(dbContext, passwordEncrypter, tokenGenerator);
        dbContext.SaveChanges();

   
        var rentalPlanTeamMember = AddRentalPlan(dbContext);
        var rentalPlanAdmMember = AddRentalPlan(dbContext);
        dbContext.SaveChanges();

    
        var rentalTeamMember = AddRental(dbContext, companyTeamMember.Id, clientTeamMember.Id, vehicleTeamMember.Id);
        var rentalAdmMember = AddRental(dbContext, companyAdmMember.Id, clientAdmMember.Id, vehicleAdmMember.Id);
        dbContext.SaveChanges();

   
        ADDRESS_TEAM_MEMBER = new AddressIdentityManager(addressTeamMember);
        ADDRESS_ADM_MEMBER = new AddressIdentityManager(addressAdmMember);
        CATEGORY_TEAM_MEMBER = new CategoryIdentitiyManager(categoryTeamMember);
        CATEGORY_ADM_MEMBER = new CategoryIdentitiyManager(categoryAdmMember);
        CLIENT_TEAM_MEMBER = new ClientIdentityManager(clientTeamMember);
        COMPANY_TEAM_MEMBER = new CompanyIdentityManager(companyTeamMember);
        COMPANY_ADM_MEMBER = new CompanyIdentityManager(companyAdmMember);
        RENTAL_PLAN_TEAM_MEMBER = new RentalPlanIdentityManager(rentalPlanTeamMember);
        RENTAL_PLAN_ADM_MEMBER = new RentalPlanIdentityManager(rentalPlanAdmMember);
        RENTAL_TEAM_MEMBER = new RentalIdentityManager(rentalTeamMember);
        RENTAL_ADM_MEMBER = new RentalIdentityManager(rentalAdmMember);
        VEHICLE_TEAM_MEMBER = new VehicleIdentitiyManager(vehicleTeamMember);
        VEHICLE_ADM_MEMBER = new VehicleIdentitiyManager(vehicleAdmMember);
}
    private User AddUserTeamMember(FleetManagerDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenGenerator)
    {
        var user = UserBuilder.Build();
        var password = user.Password;
        user.ChangePassword(passwordEncrypter.Encrypt(password));
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        var token = tokenGenerator.GenerateToken(user);
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
        USER_ADM_MEMBER = new UserIdentityManager(user, password, token);
        return user;
    }

    private static Address AddAddress(FleetManagerDbContext dbContext)
    {
        var address = AddressBuilder.Build();
        dbContext.Addresses.Add(address);
        return address;
    }
    private static Category AddCategory(FleetManagerDbContext dbContext)
    {
        var Category = CategoryBuilder.Build();
        dbContext.Categories.Add(Category);
        return Category;
    }
    private static Client AddClient(FleetManagerDbContext dbContext, long addressId)
    {
        var Client = ClientBuilder.Build(addressId);
        dbContext.Clients.Add(Client);
        return Client;
    }
    private static Company AddCompany(FleetManagerDbContext dbContext, long addressId)
    {
        var company = CompanyBuilder.Build(addressId);
        dbContext.Companies.Add(company);
        return company;
    }
    private static Rental AddRental(FleetManagerDbContext dbContext, long companyId, long clientId, long vehicleId)
    {
        var rental = RentalBuilder.Build(companyId: companyId, clientId: clientId, vehicleId: vehicleId);
        dbContext.Rentals.Add(rental);
        return rental;
    }

    private static RentalPlan AddRentalPlan(FleetManagerDbContext dbContext)
    {
        var rentalPlan = RentalPlanBuilder.Build();
        dbContext.RentalPlans.Add(rentalPlan);
        return rentalPlan;
    }

    private static Vehicle AddVehicle(FleetManagerDbContext dbContext, long categoryId)
    {
        var vehicle = VehicleBuilder.Build(categoryId);
        dbContext.Vehicles.Add(vehicle);
        return vehicle;
    }



}
