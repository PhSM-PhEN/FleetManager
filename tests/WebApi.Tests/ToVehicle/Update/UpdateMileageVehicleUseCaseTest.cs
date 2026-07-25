using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToVehicle;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToVehicle.Update
{
    public class UpdateMileageVehicleUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Vehicle";
        private readonly string _teamMemberToken;
        private readonly long _companyId;

        public UpdateMileageVehicleUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _companyId = customWebApplication.COMPANY_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var vehicleId = await RegisterVehicle();
            var request = new { MileageVehicle = 999_999 };

            var result = await DoPut($"{METHOD}/{vehicleId}", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var request = new { MileageVehicle = 1_000 };

            var result = await DoPut($"{METHOD}/0", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Mileage_Cannot_Decrease()
        {
            var vehicleId = await RegisterVehicle(currentMileage: 10_000);
            var request = new { MileageVehicle = 1_000 };

            var result = await DoPut($"{METHOD}/{vehicleId}", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("MILEAGE_CANNOT_DECREASE");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = new { MileageVehicle = 1_000 };

            var result = await DoPut($"{METHOD}/1", request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        private async Task<long> RegisterVehicle(long? currentMileage = null)
        {
            var request = RequestVehicleJsonBuilder.Build(_companyId);
            if (currentMileage.HasValue)
                request.CurrentMileage = currentMileage.Value;

            var registerResult = await DoPost(METHOD, request, _teamMemberToken);

            var body = await registerResult.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            return responseBody.RootElement.GetProperty("id").GetInt64();
        }
    }
}