using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToContract;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToContract.Update
{
    public class UpdateContractUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Contract";
        private readonly string _teamMemberToken;
        private readonly long _contractId;

        public UpdateContractUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _contractId = customWebApplication.CONTRACT_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestUpdateContractJsonBuilder.Build();

            var result = await DoPut($"{METHOD}/{_contractId}", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var request = RequestUpdateContractJsonBuilder.Build();

            var result = await DoPut($"{METHOD}/0", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_RentalType_Invalid()
        {
            var request = RequestUpdateContractJsonBuilder.Build();
            request.RentalType = "Weekly";

            var result = await DoPut($"{METHOD}/{_contractId}", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("RENTAL_TYPE_INVALID");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestUpdateContractJsonBuilder.Build();

            var result = await DoPut($"{METHOD}/{_contractId}", request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
