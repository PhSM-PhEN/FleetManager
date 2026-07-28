using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToRentalPlan;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToRentalPlan.Update
{
    public class UpdateRentalPlanUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/RentalPlan";
        private readonly string _teamMemberToken;
        private readonly long _rentalPlanId;

        public UpdateRentalPlanUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _rentalPlanId = customWebApplication.RENTAL_PLAN_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestRentalPlanJsonBuilder.Build();

            var result = await DoPut($"{METHOD}/{_rentalPlanId}", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_RentalPlan_Not_Found()
        {
            var request = RequestRentalPlanJsonBuilder.Build();

            var result = await DoPut($"{METHOD}/0", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_MonthlyPrice_Zero()
        {
            var request = RequestRentalPlanJsonBuilder.Build();
            request.MonthlyPrice = 0;

            var result = await DoPut($"{METHOD}/{_rentalPlanId}", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("MONTHLY_PRICE_INVALID");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestRentalPlanJsonBuilder.Build();

            var result = await DoPut($"{METHOD}/{_rentalPlanId}", request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
