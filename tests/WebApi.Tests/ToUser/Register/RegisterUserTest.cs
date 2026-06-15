using CommonTestUtilities.Request;
using FleetManager.Exception.ExceptionBase;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.ToUser.Register
{
    public class RegisterUserTest : FleetManagerClassFixture

    {

        private const string METHOD = "api/User";
        private readonly string _adminToken;
        private readonly string _teamMemberToken;
        private readonly string _teamMemberEmail;
        public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _ = factory.CreateClient();
            _teamMemberToken = factory.USER_TEAM_MEMBER.GetToken();
            _adminToken = factory.USER_ADM_MEMBER.GetToken();
            _teamMemberEmail = factory.USER_TEAM_MEMBER.GetEmail();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var result = await DoPost(METHOD, request, _adminToken);

            result.StatusCode.ShouldBe(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
            responseBody.RootElement.GetProperty("token").GetString().ShouldNotBeNullOrEmpty();

        }
        [Fact]

        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var result = await DoPost(METHOD, request, _adminToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);
            var errorMessage = response.RootElement.GetProperty("errorMessage").EnumerateArray();

            var expctedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_IS_REQUIRED");
            errorMessage.ShouldHaveSingleItem();
            errorMessage.ShouldContain(e => e.GetString()!.Equals(expctedMessage));
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            request.Email = _teamMemberEmail;

            var result = await DoPost(METHOD, request, _adminToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);
            var errorMessage = response.RootElement.GetProperty("errorMessage").EnumerateArray();

            var expctedMessage = ResourceErrorMessages.ResourceManager.GetString("EMAIL_ALREADY_REGISTERED");
            errorMessage.ShouldHaveSingleItem();
            errorMessage.ShouldContain(e => e.GetString()!.Equals(expctedMessage));
        }
    }
}
