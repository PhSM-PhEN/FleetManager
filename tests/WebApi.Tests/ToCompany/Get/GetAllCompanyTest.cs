using Shouldly;
using System.Text.Json;

namespace WebApi.Tests.ToCompany.Get
{
    public class GetAllCompanyTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Company";
        private readonly HttpClient _;
        private readonly string _TeamMemberToken;
        private readonly string _AdminToken;
        public GetAllCompanyTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _ = customWebApplication.CreateClient();
            _TeamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _AdminToken = customWebApplication.USER_ADM_MEMBER.GetToken();
        }
        [Fact]
        public async Task Success_Team_Member_Token()
        {
            var result = await DoGet(METHOD, _TeamMemberToken);
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var expectedMessage = responseBody.RootElement.GetProperty("companies");
            expectedMessage.EnumerateArray().ShouldNotBeEmpty();  

        }
        [Fact]
        public async Task Success_Admin_Token()
        {
            var result = await DoGet(METHOD, _AdminToken);
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var expectedMessage = responseBody.RootElement.GetProperty("companies");
            expectedMessage.EnumerateArray().ShouldNotBeEmpty();  

        }
        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoGet(METHOD);
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.Unauthorized);

        }


    }
}
