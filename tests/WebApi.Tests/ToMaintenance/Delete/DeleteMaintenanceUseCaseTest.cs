using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToMaintenance;
using Shouldly;

namespace WebApi.Tests.ToMaintenance.Delete
{
    public class DeleteMaintenanceUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Maintenance";
        private readonly string _adminToken;
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;

        public DeleteMaintenanceUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _adminToken = customWebApplication.USER_ADM.GetToken();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _vehicleId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestMaintenanceJsonBuilder.Build(_vehicleId);
            var registerResult = await DoPost(METHOD, request, _teamMemberToken);

            var body = await registerResult.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);
            var maintenanceId = responseBody.RootElement.GetProperty("id").GetInt64();

            var result = await DoDelete($"{METHOD}/{maintenanceId}", _adminToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Maintenance_Not_Found()
        {
            var result = await DoDelete($"{METHOD}/0", _adminToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoDelete($"{METHOD}/1");
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Forbidden_For_Team_Member()
        {
            var result = await DoDelete($"{METHOD}/1", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }
    }
}
