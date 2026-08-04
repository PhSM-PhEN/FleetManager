using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToRentalPlan;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToRentalPlan.Register
{
    public class RegisterRentalPlanUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/RentalPlan";
        private readonly string _adminToken;
        private readonly string _teamMemberToken;

        public RegisterRentalPlanUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _adminToken = customWebApplication.USER_ADM.GetToken();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestRentalPlanJsonBuilder.Build();

            var result = await DoPost(METHOD, request, _adminToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRentalPlanJsonBuilder.Build();
            request.Name = string.Empty;

            var result = await DoPost(METHOD, request, _adminToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_IS_REQUIRED");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_DailyPrice_Zero()
        {
            var request = RequestRentalPlanJsonBuilder.Build();
            request.DailyPrice = 0;

            var result = await DoPost(METHOD, request, _adminToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("DAILY_PRICE_INVALID");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestRentalPlanJsonBuilder.Build();

            var result = await DoPost(METHOD, request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Forbidden_For_Team_Member()
        {
            var request = RequestRentalPlanJsonBuilder.Build();

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }
    }
}
