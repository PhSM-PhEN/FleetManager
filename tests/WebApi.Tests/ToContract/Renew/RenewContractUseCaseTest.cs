using System.Net;
using CommonTestUtilities.Request.ToContract;
using Shouldly;

namespace WebApi.Tests.ToContract.Renew
{
    public class RenewContractUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Contract";
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;
        private readonly long _tenantId;
        private readonly long _rentalPlanId;
        private readonly long _seededContractId;

        public RenewContractUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
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
            var contractId = await RegisterAndActivateContract();

            var request = RequestRenewContractJsonBuilder.Build();
            var result = await DoPost($"{METHOD}/{contractId}/Renew", request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.Created);

            var renewedContractId = await GetIdFromResponse(result);
            await DoPatch($"{METHOD}/{renewedContractId}/Cancel", _teamMemberToken);
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var request = RequestRenewContractJsonBuilder.Build();
            var result = await DoPost($"{METHOD}/0/Renew", request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Contract_Not_Active()
        {
 
            var request = RequestRenewContractJsonBuilder.Build();
            var result = await DoPost($"{METHOD}/{_seededContractId}/Renew", request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task Error_MileageContracted_Invalid()
        {
            var contractId = await RegisterAndActivateContract();

            var request = RequestRenewContractJsonBuilder.Build(mileageContracted: -1);
            var result = await DoPost($"{METHOD}/{contractId}/Renew", request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            await DoPatch($"{METHOD}/{contractId}/Cancel", _teamMemberToken);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestRenewContractJsonBuilder.Build();
            var result = await DoPost($"{METHOD}/1/Renew", request);

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
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode == false)
                throw new Exception($"Esperava sucesso mas veio {(int)response.StatusCode} {response.StatusCode}. Corpo: {body}");

            var responseBody = System.Text.Json.JsonDocument.Parse(body);
            return responseBody.RootElement.GetProperty("id").GetInt64();
        }
    }
}