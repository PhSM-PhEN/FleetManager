using CommonTestUtilities.Entitie;
using CommonTestUtilities.Repositories;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Security.Cryptography;
using FleetManager.Domain.Security.Token;
using FleetManager.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using WebApi.Tests.Resource;



namespace WebApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program> 
{
    public RentalIdentityManager RENTAL_TEAM_MEMBER { get ; private set ;} = default!;
    public RentalIdentityManager RENTAL_ADM_MEMBER { get ; private set ;} = default!;
    public UserIdentityManager USER_TEAM_MEMBER { get ; private set ;} = default!;
    public UserIdentityManager USER_ADM_MEMBER {get ; private set ;} = default!;
    private void StartDataBase(FleetManagerDbContext dbContext, IPasswordEncrypter passwordEncrypter, ITokenProvider tokenProvider)
    {
        
    }
    private User AddUserTeamBember(FleetManagerDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenGenerator)
    {
        var user = UserBuilder.Build();
        user.Id = 1;
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
        user.Id = 2;
        var password = user.Password;
        user.ChangePassword(passwordEncrypter.Encrypt(password));
        dbContext.Users.Add(user);

        var token = tokenGenerator.GenerateToken(user);
        USER_ADM_MEMBER = new UserIdentityManager(user, password, token);
        return user;
    }
    //private Rental AddRental(FleetManagerDbContext dbContext, User user, long rentalId)
         
    
}
