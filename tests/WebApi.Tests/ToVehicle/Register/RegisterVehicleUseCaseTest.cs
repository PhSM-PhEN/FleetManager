using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToVehicle;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToVehicle.Register
{
    public class RegisterVehicleUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Vehicle";
        private readonly string _teamMemberToken;
        private readonly long _companyId;

        public RegisterVehicleUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _companyId = customWebApplication.COMPANY_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestVehicleJsonBuilder.Build(_companyId);

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("model").GetString().ShouldBe(request.Model);
        }

        [Fact]
        public async Task Error_Brand_Empty()
        {
            var request = RequestVehicleJsonBuilder.Build(_companyId);
            request.Brand = string.Empty;

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("BRAND_REQUIRED");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Company_Not_Found()
        {
            var request = RequestVehicleJsonBuilder.Build(999);

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestVehicleJsonBuilder.Build(_companyId);

            var result = await DoPost(METHOD, request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}