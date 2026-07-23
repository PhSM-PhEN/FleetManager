using System.Net;
using CommonTestUtilities.Request.ToTenant;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToTenant.Update
{
    public class UpdateTenantUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Tenant";
        private readonly string _teamMemberToken;
        private readonly long _addressId;
        private readonly long _tenantId;

        public UpdateTenantUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _addressId = customWebApplication.ADDRESS_TEAM_MEMBER.GetById();
            _tenantId = customWebApplication.TENANT_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestUpdateTenantJsonBuilder.Build(_addressId);

            var result = await DoPut($"{METHOD}/{_tenantId}", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Tenant_Not_Found()
        {
            var request = RequestUpdateTenantJsonBuilder.Build(_addressId);

            var result = await DoPut($"{METHOD}/0", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_PhoneNumber_Empty()
        {
            var request = RequestUpdateTenantJsonBuilder.Build(_addressId);
            request.PhoneNumber = string.Empty;

            var result = await DoPut($"{METHOD}/{_tenantId}", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestUpdateTenantJsonBuilder.Build(_addressId);

            var result = await DoPut($"{METHOD}/{_tenantId}", request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}