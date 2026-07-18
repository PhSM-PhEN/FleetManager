using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToAddress;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToAddress.Register
{
    public class RegisterAddressUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Address";
        private readonly string _teamMemberToken;

        public RegisterAddressUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestAddressJsonBuilder.Build();

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("street").GetString().ShouldBe(request.Street);
            responseBody.RootElement.GetProperty("zipCode").GetString().ShouldBe(request.ZipCode);
        }

        [Fact]
        public async Task Error_Street_Empty()
        {
            var request = RequestAddressJsonBuilder.Build();
            request.Street = string.Empty;

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("STREET_REQUIRED");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestAddressJsonBuilder.Build();

            var result = await DoPost(METHOD, request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
