using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToCompany;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToCompany.Register
{
    public class RegisterCompanyUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Company";
        private readonly string _teamMemberToken;
        private readonly long _addressId;

        public RegisterCompanyUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _addressId = customWebApplication.ADDRESS_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestCompanyJsonBuilder.Build(_addressId);

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestCompanyJsonBuilder.Build(_addressId);
            request.Name = string.Empty;

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_IS_REQUIRED");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Address_Not_Found()
        {
            var request = RequestCompanyJsonBuilder.Build(999);

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestCompanyJsonBuilder.Build(_addressId);

            var result = await DoPost(METHOD, request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}