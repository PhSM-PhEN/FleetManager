using System;
using CommonTestUtilities.Entitie;
using FleetManager.Communication.Requests;

namespace WebApi.Tests.ToUser.Update
{
    public class UdateUserTest : FleetManagerClassFixture
    {
        private readonly HttpClient _;
        private const string METHOD = "api/User";
        private readonly string _adminToken;
        private readonly string _teamMemberToken;
        public UdateUserTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _ = customWebApplication.CreateClient();
            _adminToken = customWebApplication.USER_ADM_MEMBER.GetToken();
            _teamMemberToken = customWebApplication.USER_ADM_MEMBER.GetToken();
            
        }
        public async Task Success()
        {
            var user = UserBuilder.Build();
            //var request = RequestUpdateUserJsonBuilder()

        }

    }
}


