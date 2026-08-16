using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToMaintenance;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToMaintenance.Close
{
    public class CloseMaintenanceUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Maintenance";
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;

        public CloseMaintenanceUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _vehicleId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var registerRequest = RequestMaintenanceJsonBuilder.Build(_vehicleId);
            var registerResult = await DoPost(METHOD, registerRequest, _teamMemberToken);

            var registerBody = await registerResult.Content.ReadAsStreamAsync();
            var registerResponse = await JsonDocument.ParseAsync(registerBody);
            var maintenanceId = registerResponse.RootElement.GetProperty("id").GetInt64();

            var closeRequest = RequestClosedMaintenanceJsonBuilder.Build(workshopBudget: 750, problemDescription: "Engine oil leak");

            var result = await DoPatch($"{METHOD}/{maintenanceId}/Close", closeRequest, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("id").GetInt64().ShouldBe(maintenanceId);
            responseBody.RootElement.GetProperty("status").GetString().ShouldBe("Closed");
            responseBody.RootElement.GetProperty("workshopBudget").GetDecimal().ShouldBe(750);
        }

        [Fact]
        public async Task Error_Already_Closed()
        {
            var registerRequest = RequestMaintenanceJsonBuilder.Build(_vehicleId);
            var registerResult = await DoPost(METHOD, registerRequest, _teamMemberToken);

            var registerBody = await registerResult.Content.ReadAsStreamAsync();
            var registerResponse = await JsonDocument.ParseAsync(registerBody);
            var maintenanceId = registerResponse.RootElement.GetProperty("id").GetInt64();

            var closeRequest = RequestClosedMaintenanceJsonBuilder.Build();
            await DoPatch($"{METHOD}/{maintenanceId}/Close", closeRequest, _teamMemberToken);

            var result = await DoPatch($"{METHOD}/{maintenanceId}/Close", closeRequest, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Conflict);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("MAINTENANCE_IS_ALREADY_CLOSED");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Maintenance_Not_Found()
        {
            var closeRequest = RequestClosedMaintenanceJsonBuilder.Build();

            var result = await DoPatch($"{METHOD}/0/Close", closeRequest, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var closeRequest = RequestClosedMaintenanceJsonBuilder.Build();

            var result = await DoPatch($"{METHOD}/1/Close", closeRequest);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
