using System.Net;
using System.Text.Json;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToTenant.GetById
{
    public class GetByIdTenantUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Tenant";
        private readonly string _teamMemberToken;
        private readonly long _tenantId;

        public GetByIdTenantUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _tenantId = customWebApplication.TENANT_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoGet($"{METHOD}/{_tenantId}", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("id").GetInt64().ShouldBe(_tenantId);
        }

        [Fact]
        public async Task Error_Tenant_Not_Found()
        {
            var result = await DoGet($"{METHOD}/0", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("TENANT_NOT_FOUND");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoGet($"{METHOD}/{_tenantId}");
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}