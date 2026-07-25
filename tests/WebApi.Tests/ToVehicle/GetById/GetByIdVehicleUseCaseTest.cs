using System.Net;
using System.Text.Json;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToVehicle.GetById
{
    public class GetByIdVehicleUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Vehicle";
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;

        public GetByIdVehicleUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _vehicleId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoGet($"{METHOD}/{_vehicleId}", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("id").GetInt64().ShouldBe(_vehicleId);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var result = await DoGet($"{METHOD}/0", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("VEHICLE_NOT_FOUND");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoGet($"{METHOD}/{_vehicleId}");
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}