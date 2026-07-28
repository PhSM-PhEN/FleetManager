using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToRentalPlan;
using Shouldly;

namespace WebApi.Tests.ToRentalPlan.Delete
{
    public class DeleteRentalPlanUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/RentalPlan";
        private readonly string _teamMemberToken;

        public DeleteRentalPlanUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            // Registra um plano próprio (sem veículos vinculados) para não interferir
            // no plano semeado pela fábrica (RENTAL_PLAN_TEAM_MEMBER), que outros testes usam.
            var request = RequestRentalPlanJsonBuilder.Build();
            var registerResult = await DoPost(METHOD, request, _teamMemberToken);

            var body = await registerResult.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);
            var rentalPlanId = responseBody.RootElement.GetProperty("id").GetInt64();

            var result = await DoDelete($"{METHOD}/{rentalPlanId}", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_RentalPlan_Not_Found()
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
