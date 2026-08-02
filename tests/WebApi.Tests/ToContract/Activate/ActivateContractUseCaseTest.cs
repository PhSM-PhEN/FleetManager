using System.Net;
using CommonTestUtilities.Request.ToContract;
using Shouldly;

namespace WebApi.Tests.ToContract.Activate
{
    public class ActivateContractUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Contract";
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;
        private readonly long _tenantId;
        private readonly long _rentalPlanId;
        private readonly long _seededContractId;

        public ActivateContractUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
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
            // O contrato semeado pela fábrica já nasce Reserved — dá pra ativar direto.
            var result = await DoPatch($"{METHOD}/{_seededContractId}/Activate", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var result = await DoPatch($"{METHOD}/0/Activate", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Contract_Not_Reserved()
        {
            // Registra e ativa um contrato próprio (veículo livre, sem mexer no contrato semeado
            // usado no teste de sucesso acima) e tenta ativar de novo: já não está mais Reserved.
            var request = RequestContractJsonBuilder.Build(_vehicleId, _tenantId, _rentalPlanId);
            var registerResult = await DoPost(METHOD, request, _teamMemberToken);
            var contractId = await GetIdFromResponse(registerResult);

            var firstActivate = await DoPatch($"{METHOD}/{contractId}/Activate", _teamMemberToken);
            firstActivate.StatusCode.ShouldBe(HttpStatusCode.NoContent);

            var result = await DoPatch($"{METHOD}/{contractId}/Activate", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoPatch($"{METHOD}/1/Activate");
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
