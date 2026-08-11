using System.Net;
using CommonTestUtilities.Request.ToContract;
using Shouldly;

namespace WebApi.Tests.ToContract.FinishUp
{
    public class FinishUpContractUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Contract";
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;
        private readonly long _tenantId;
        private readonly long _rentalPlanId;
        private readonly long _vehicleCurrentMileage;
        private readonly long _seededContractId;

        public FinishUpContractUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _vehicleId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
            _tenantId = customWebApplication.TENANT_TEAM_MEMBER.GetById();
            _rentalPlanId = customWebApplication.RENTAL_PLAN_TEAM_MEMBER.GetById();
            _vehicleCurrentMileage = customWebApplication.VEHICLE_TEAM_MEMBER.GetCurrentMileage();
            _seededContractId = customWebApplication.CONTRACT_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var contractId = await RegisterAndActivateContract();

            var request = RequestFinishUpContractJsonBuilder.Build(_vehicleCurrentMileage + 100);
            var result = await DoPatch($"{METHOD}/{contractId}/FinishUp", request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var request = RequestFinishUpContractJsonBuilder.Build(_vehicleCurrentMileage + 100);
            var result = await DoPatch($"{METHOD}/0/FinishUp", request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Contract_Not_Active()
        {
            // Contrato semeado está Reserved: finish up só é permitido para Active/Overdue.
            var request = RequestFinishUpContractJsonBuilder.Build(_vehicleCurrentMileage + 100);
            var result = await DoPatch($"{METHOD}/{_seededContractId}/FinishUp", request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task Error_FinalMileage_Negative()
        {
            var contractId = await RegisterAndActivateContract();

            var request = RequestFinishUpContractJsonBuilder.Build(-1);
            var result = await DoPatch($"{METHOD}/{contractId}/FinishUp", request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestFinishUpContractJsonBuilder.Build(_vehicleCurrentMileage + 100);
            var result = await DoPatch($"{METHOD}/1/FinishUp", request);

            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        private async Task<long> RegisterAndActivateContract()
        {
            var request = RequestContractJsonBuilder.Build(_vehicleId, _tenantId, _rentalPlanId);
            var registerResult = await DoPost(METHOD, request, _teamMemberToken);
            var contractId = await GetIdFromResponse(registerResult);

            await DoPatch($"{METHOD}/{contractId}/Activate", _teamMemberToken);

            return contractId;
        }

        private static async Task<long> GetIdFromResponse(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStreamAsync();
            var responseBody = await System.Text.Json.JsonDocument.ParseAsync(body);
            return responseBody.RootElement.GetProperty("id").GetInt64();
        }
    }
}
