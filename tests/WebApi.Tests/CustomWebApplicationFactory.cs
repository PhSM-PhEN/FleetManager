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
    public RentalIdentityManager RENTAL_TEAM_MEMBER { get ; private set ;} = default!;
    public RentalIdentityManager RENTAL_ADM_MEMBER { get ; private set ;} = default!;
    public UserIdentityManager USER_TEAM_MEMBER { get ; private set ;} = default!;
    public UserIdentityManager USER_ADM_MEMBER {get ; private set ;} = default!;

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
    var address = AddAddress(dbContext);
    dbContext.SaveChanges();

    var company = AddCompany(dbContext, address.Id);
    var category = AddCategory(dbContext);
    dbContext.SaveChanges();

    var vehicle = AddVehicle(dbContext, category.Id);
    var client = AddClient(dbContext, address.Id);
    var userTeamMember = AddUserTeamMember(dbContext, passwordEncrypter, tokenGenerator);
    var userAdmin = AddUserAdmin(dbContext, passwordEncrypter, tokenGenerator);
    dbContext.SaveChanges();

    var rentalTeamMember = AddRental(dbContext, userTeamMember, company.Id, client.Id, vehicle.Id);
    var rentalAdmin = AddRental(dbContext, userAdmin, company.Id, client.Id, vehicle.Id);

    RENTAL_TEAM_MEMBER = new RentalIdentityManager(rentalTeamMember);
    RENTAL_ADM_MEMBER = new RentalIdentityManager(rentalAdmin);

    dbContext.SaveChanges();
    }
    private User AddUserTeamMember(FleetManagerDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenGenerator)
    {
        var user = UserBuilder.Build();
        var password = user.Password;
        user.ChangePassword(passwordEncrypter.Encrypt(password));
        dbContext.Users.Add(user);

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
    private static Rental AddRental(FleetManagerDbContext dbContext, User user, int companyId, long clientId, long vehicleId)
    {
        var rental = RentalBuilder.Build(userId: user.Id, companyId: companyId, clientId: clientId, vehicleId: vehicleId);
        dbContext.Rentals.Add(rental);
        return rental;
    }

    private static RentalPlan AddRentalPlan(FleetManagerDbContext dbContext)
    {
        var rentalPlan = RentalPlanBuilder.Build();
        dbContext.RentalPlans.Add(rentalPlan);
        return rentalPlan;
    }

    private static Vehicle AddVehicle(FleetManagerDbContext dbContext, int categoryId)
    {
        var vehicle = VehicleBuilder.Build(categoryId);
        dbContext.Vehicles.Add(vehicle);
        return vehicle;
    }



}
