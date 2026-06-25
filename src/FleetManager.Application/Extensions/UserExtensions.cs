using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class UserExtensions
    {
        public static ResponseUserProfileJson ToResponse(this User user)
        {
            return new ResponseUserProfileJson
            {
                Name = user.Name,
                Email = user.Email
            };
        }
        public static ResponseLoginJson ToLoginResponse(this User user, string token)
        {
            return new ResponseLoginJson
            {
                Name = user.Name,
                Token = token   
            };         
        }
    }
}
