using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToTenant;
using Shouldly;

namespace WebApi.Tests.ToTenant.Delete
{
    public class DeleteTenantUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Tenant";
        private readonly string _teamMemberToken;
        private readonly long _addressId;

        public DeleteTenantUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _addressId = customWebApplication.ADDRESS_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestTenantJsonBuilder.Build(_addressId);
            var registerResult = await DoPost(METHOD, request, _teamMemberToken);

            var body = await registerResult.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);
            var tenantId = responseBody.RootElement.GetProperty("id").GetInt64();

            var result = await DoDelete($"{METHOD}/{tenantId}", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Tenant_Not_Found()
        {
            var result = await DoDelete($"{METHOD}/0", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoDelete($"{METHOD}/1");
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}