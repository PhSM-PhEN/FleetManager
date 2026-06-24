using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{
    public class UserIdentityManager(User user, string password, string token)
    {

        public string GetName() => user.Name;
        public string GetEmail() => user.Email;
        public string GetPassword() => password;
        public string GetToken() => token;

    }   
}