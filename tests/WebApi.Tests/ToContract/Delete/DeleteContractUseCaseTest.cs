using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToContract;
using Shouldly;

namespace WebApi.Tests.ToContract.Delete
{
    public class DeleteContractUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Contract";
        private readonly string _adminToken;
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;
        private readonly long _tenantId;
        private readonly long _rentalPlanId;

        public DeleteContractUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _adminToken = customWebApplication.USER_ADM.GetToken();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _vehicleId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
            _tenantId = customWebApplication.TENANT_TEAM_MEMBER.GetById();
            _rentalPlanId = customWebApplication.RENTAL_PLAN_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            // Registra um contrato proprio (veiculo sem contrato ativo) para nao interferir
            // no contrato semeado pela fabrica (CONTRACT_TEAM_MEMBER), que outros testes usam.
            var request = RequestContractJsonBuilder.Build(_vehicleId, _tenantId, _rentalPlanId);
            var registerResult = await DoPost(METHOD, request, _teamMemberToken);

            var body = await registerResult.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);
            var contractId = responseBody.RootElement.GetProperty("id").GetInt64();

            var result = await DoDelete($"{METHOD}/{contractId}", _adminToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var result = await DoDelete($"{METHOD}/0", _adminToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoDelete($"{METHOD}/1");
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Forbidden_For_Team_Member()
        {
            var result = await DoDelete($"{METHOD}/1", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }
    }
}
