using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToMaintenance;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToMaintenance.Register
{
    public class RegisterMaintenanceUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Maintenance";
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;

        public RegisterMaintenanceUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _vehicleId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestMaintenanceJsonBuilder.Build(_vehicleId);

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("vehicleId").GetInt64().ShouldBe(_vehicleId);
        }

        [Fact]
        public async Task Error_VehicleId_Zero()
        {
            var request = RequestMaintenanceJsonBuilder.Build(0);

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("VEHICLE_ID_REQUIRED");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var request = RequestMaintenanceJsonBuilder.Build(999);

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestMaintenanceJsonBuilder.Build(_vehicleId);

            var result = await DoPost(METHOD, request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
