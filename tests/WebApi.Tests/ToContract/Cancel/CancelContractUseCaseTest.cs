using System.Net;
using CommonTestUtilities.Request.ToContract;
using Shouldly;

namespace WebApi.Tests.ToContract.Cancel
{
    public class CancelContractUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Contract";
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;
        private readonly long _tenantId;
        private readonly long _rentalPlanId;
        private readonly long _seededContractId;

        public CancelContractUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _vehicleId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
            _tenantId = customWebApplication.TENANT_TEAM_MEMBER.GetById();
            _rentalPlanId = customWebApplication.RENTAL_PLAN_TEAM_MEMBER.GetById();
            _seededContractId = customWebApplication.CONTRACT_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            // Registra um contrato próprio (veículo livre) — deixa Cancelado ao final, sem
            // interferir no contrato semeado pela fábrica, usado no teste de regra de negócio abaixo.
            var request = RequestContractJsonBuilder.Build(_vehicleId, _tenantId, _rentalPlanId);
            var registerResult = await DoPost(METHOD, request, _teamMemberToken);
            var contractId = await GetIdFromResponse(registerResult);

            var result = await DoPatch($"{METHOD}/{contractId}/Cancel", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var result = await DoPatch($"{METHOD}/0/Cancel", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Contract_Not_Active()
        {
            // Cancela o contrato semeado (Reserved -> Cancelled) e tenta cancelar de novo:
            // um contrato já cancelado não pode ser cancelado outra vez.
            var firstCancel = await DoPatch($"{METHOD}/{_seededContractId}/Cancel", _teamMemberToken);
            firstCancel.StatusCode.ShouldBe(HttpStatusCode.NoContent);

            var result = await DoPatch($"{METHOD}/{_seededContractId}/Cancel", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoPatch($"{METHOD}/1/Cancel");
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        private static async Task<long> GetIdFromResponse(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStreamAsync();
            var responseBody = await System.Text.Json.JsonDocument.ParseAsync(body);
            return responseBody.RootElement.GetProperty("id").GetInt64();
        }
    }
}
