using System.Net;
using Shouldly;

namespace WebApi.Tests.ToUser.Promote
{
    public class PromoteUserUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/User/Promote";
        private readonly string _teamMemberToken;

        public PromoteUserUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
        }

        [Fact]
        public async Task Error_Admin_Already_Exists()
        {
            var result = await DoPatch(METHOD, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Error_WhitOut_Token()
        {
            var result = await DoPatch(METHOD);

            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}