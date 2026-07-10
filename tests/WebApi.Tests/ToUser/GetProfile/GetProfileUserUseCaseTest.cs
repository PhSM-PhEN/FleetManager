using Shouldly;
using System.Text.Json;

namespace WebApi.Tests.ToUser.GetProfile
{
    public class GetProfileUserUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/User/Profile";
        private readonly string _teamMemberToken;
        private readonly string _teamMemberEmail;
        private readonly string _teamMemberName;

        public GetProfileUserUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _teamMemberEmail = customWebApplication.USER_TEAM_MEMBER.GetEmail();
            _teamMemberName = customWebApplication.USER_TEAM_MEMBER.GetName();
        }
        [Fact]
        public async Task Sucess()
        {
            var result = await DoGet(METHOD, _teamMemberToken);

            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("name").GetString().ShouldBe(_teamMemberName);
            responseBody.RootElement.GetProperty("email").ToString().ShouldBe(_teamMemberEmail);
        }
    }
}
