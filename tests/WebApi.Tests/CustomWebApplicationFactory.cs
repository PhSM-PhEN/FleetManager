using CommonTestUtilities.Entitie;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Security.Cryptography;
using FleetManager.Domain.Security.Token;
using FleetManager.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
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
                    config.UseInMemoryDatabase("InMemoryDbForTesting")
                        .UseInternalServiceProvider(provider);
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
        var userTeamMember = AddUserTeamMember(dbContext, passwordEncrypter, tokenGenerator);
        var userAdmin = AddUserAdmin(dbContext, passwordEncrypter, tokenGenerator);
        dbContext.SaveChanges(); 

        var rentalTeamMember = AddRental(dbContext, userTeamMember);
        var rentalAdmin = AddRental(dbContext, userAdmin);

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
    private Rental AddRental(FleetManagerDbContext dbContext, User user)
    {
        var rental = RentalBuilder.Build(userId: user.Id);
        dbContext.Rentals.Add(rental);
        return rental;
    }


}
