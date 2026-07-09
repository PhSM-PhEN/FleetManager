using FleetManager.Communication.Response.ToUser;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class UserExtensions
    {
        public static ResponseLoginUserJson ToLoginResponse(this User user, string token)
        {
            return new ResponseLoginUserJson
            {
                Name = user.Name,
                Token = token
            };
        }
        public static ResponseProfileUserJson ToProfileResponse(this User user)
        {
            return new ResponseProfileUserJson
            {
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
