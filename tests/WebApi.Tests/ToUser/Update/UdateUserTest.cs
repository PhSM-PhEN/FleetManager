using System.Net;
using CommonTestUtilities.Request;
using Shouldly;

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
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            
        }
        [Fact]
        public async Task Success()
        {
            
            var request = RequestUpdateUserJsonBuilder.Build();

            var result = await DoPut(METHOD, request, _adminToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        }
        [Fact]
        public async Task Error_WhitOut_Token()
        {
            var request = RequestUpdateUserJsonBuilder.Build();

            var result = await DoPut(METHOD, request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
            
        }
        [Fact]
        public async Task Error_TeamMember_Token()
        {
            var request = RequestUpdateUserJsonBuilder.Build();

            var result = await DoPut(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }

    }
}


