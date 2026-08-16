using System.Net;
using System.Text.Json;
using Shouldly;

namespace WebApi.Tests.ToMaintenance.GetAll
{
    public class GetAllMaintenanceUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Maintenance";
        private readonly string _teamMemberToken;

        public GetAllMaintenanceUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
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
        }

        [Fact]
        public async Task Success_Respects_PageSize()
        {
            var result = await DoGet($"{METHOD}?pageNumber=1&pageSize=1", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("pageSize").GetInt32().ShouldBe(1);
            responseBody.RootElement.GetProperty("data").GetArrayLength().ShouldBeLessThanOrEqualTo(1);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoGet(METHOD);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
