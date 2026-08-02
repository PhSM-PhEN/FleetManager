using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToContract;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToContract.Preview
{
    public class PreviewContractUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Contract/Preview";
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;
        private readonly long _tenantId;
        private readonly long _rentalPlanId;

        public PreviewContractUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _vehicleId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
            _tenantId = customWebApplication.TENANT_TEAM_MEMBER.GetById();
            _rentalPlanId = customWebApplication.RENTAL_PLAN_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            // Não persiste nada — pode chamar quantas vezes quiser sem ocupar o veículo
            // pros outros testes da classe.
            var request = RequestContractJsonBuilder.Build(_vehicleId, _tenantId, _rentalPlanId);

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("id").ValueKind.ShouldBe(JsonValueKind.Null);
            responseBody.RootElement.GetProperty("totalAmount").GetDecimal().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Error_VehicleId_Required()
        {
            var request = RequestContractJsonBuilder.Build(0, _tenantId, _rentalPlanId);

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("VEHICLE_ID_REQUIRED");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestContractJsonBuilder.Build(_vehicleId, _tenantId, _rentalPlanId);

            var result = await DoPost(METHOD, request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
