using System.Net;
using System.Text.Json;
using Shouldly;

namespace WebApi.Tests.ToTenant.GetAll
{
    public class GetAllTenantUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Tenant";
        private readonly string _teamMemberToken;

        public GetAllTenantUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
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

            responseBody.RootElement.GetProperty("data").GetArrayLength().ShouldBeGreaterThan(0);
            responseBody.RootElement.GetProperty("pageNumber").GetInt32().ShouldBe(1);
            responseBody.RootElement.GetProperty("pageSize").GetInt32().ShouldBe(10);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoGet(METHOD);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}