using System.Net;
using System.Text.Json;
using Shouldly;

namespace WebApi.Tests.ToCompany.GetAll
{
    public class GetAllCompanyUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Company";
        private readonly string _teamMemberToken;

        public GetAllCompanyUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoGet(METHOD, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetArrayLength().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoGet(METHOD);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}