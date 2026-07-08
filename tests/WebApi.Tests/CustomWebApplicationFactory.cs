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
