using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToVehicle;
using Shouldly;

namespace WebApi.Tests.ToVehicle.Delete
{
    public class DeleteVehicleUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Vehicle";
        private readonly string _teamMemberToken;
        private readonly long _companyId;
        private readonly long _rentalPlanId;

        public DeleteVehicleUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _companyId = customWebApplication.COMPANY_TEAM_MEMBER.GetById();
            _rentalPlanId = customWebApplication.RENTAL_PLAN_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestVehicleJsonBuilder.Build(_companyId, _rentalPlanId);
            var registerResult = await DoPost(METHOD, request, _teamMemberToken);

            var body = await registerResult.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);
            var vehicleId = responseBody.RootElement.GetProperty("id").GetInt64();

            var result = await DoDelete($"{METHOD}/{vehicleId}", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
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