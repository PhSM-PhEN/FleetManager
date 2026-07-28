using System.Net;
using System.Text.Json;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToRentalPlan.GetById
{
    public class GetByIdRentalPlanUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/RentalPlan";
        private readonly string _teamMemberToken;
        private readonly long _rentalPlanId;

        public GetByIdRentalPlanUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _rentalPlanId = customWebApplication.RENTAL_PLAN_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoGet($"{METHOD}/{_rentalPlanId}", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("id").GetInt64().ShouldBe(_rentalPlanId);
        }

        [Fact]
        public async Task Error_RentalPlan_Not_Found()
        {
            var result = await DoGet($"{METHOD}/0", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("RENTAL_PLAN_NOT_FOUND");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoGet($"{METHOD}/{_rentalPlanId}");
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
