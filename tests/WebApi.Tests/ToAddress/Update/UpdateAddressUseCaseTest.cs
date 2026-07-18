using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToAddress;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToAddress.Update
{
    public class UpdateAddressUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Address";
        private readonly string _teamMemberToken;

        public UpdateAddressUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            var addressId = await RegisterAddress();
            var request = RequestAddressJsonBuilder.Build();

            var result = await DoPut($"{METHOD}/{addressId}", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Address_Not_Found()
        {
            var request = RequestAddressJsonBuilder.Build();

            var result = await DoPut($"{METHOD}/0", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("ADDRESS_NOT_FOUND");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Street_Empty()
        {
            var addressId = await RegisterAddress();
            var request = RequestAddressJsonBuilder.Build();
            request.Street = string.Empty;

            var result = await DoPut($"{METHOD}/{addressId}", request, _teamMemberToken);
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

            var result = await DoPut($"{METHOD}/1", request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        private async Task<long> RegisterAddress()
        {
            var request = RequestAddressJsonBuilder.Build();
            var registerResult = await DoPost(METHOD, request, _teamMemberToken);

            var body = await registerResult.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            return responseBody.RootElement.GetProperty("id").GetInt64();
        }
    }
}
