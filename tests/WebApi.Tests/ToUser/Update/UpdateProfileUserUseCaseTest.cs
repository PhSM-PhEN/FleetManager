
using System.Net;
using CommonTestUtilities.Request.ToUser;
using Shouldly;

namespace WebApi.Tests.ToUser.Update
{
    public class UpdateProfileUserUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/User";
        private readonly string _teamMemberToken;
        public UpdateProfileUserUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
        }

        [Fact]
        public async Task Success()
        {

            var request = RequestUpdateUserJsonBuilder.Build();

            var result = await DoPut(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        }
        [Fact]
        public async Task Error_WhitOut_Token()
        {
            var request = RequestUpdateUserJsonBuilder.Build();

            var result = await DoPut(METHOD, request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

        }




    }
}
