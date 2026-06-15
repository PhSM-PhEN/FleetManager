using Shouldly;

namespace WebApi.Tests.ToUser.GetProfile
{
    public class GetUerProfileTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/User/";
        private readonly string _teamMemberToken;
        private readonly string _teamMemberEmail;
        private readonly string _teamMemberName;
        public GetUerProfileTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _teamMemberEmail = customWebApplication.USER_TEAM_MEMBER.GetEmail();
            _teamMemberName = customWebApplication.USER_TEAM_MEMBER.GetName();
        }
        [Fact]
        public async Task Success()
        {
            var result = await DoGet(METHOD, _teamMemberToken);
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await System.Text.Json.JsonDocument.ParseAsync(body);
            responseBody.RootElement.GetProperty("name").GetString().ShouldBe(_teamMemberName);
            responseBody.RootElement.GetProperty("email").GetString().ShouldBe(_teamMemberEmail);
        }
    }
}
