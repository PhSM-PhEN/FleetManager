using System.Text.Json;
using CommonTestUtilities.Request.ToUser;
using FleetManager.Communication.Request.ToUser;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToUser.Register
{
    public class RegisterUserUseCaseTest : FleetManagerClassFixture
    {
        private readonly HttpClient _client;
        private const string METHOD = "api/User";
        private readonly string _teamMemberToken;
        private readonly string _teamMemberEmail;
        public RegisterUserUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _client = customWebApplication.CreateClient();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _teamMemberEmail = customWebApplication.USER_TEAM_MEMBER.GetEmail();

        }
        [Fact]
        public async Task Success()
        {

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = await DoPost(METHOD, request);
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
            responseBody.RootElement.GetProperty("token").GetString().ShouldNotBeNullOrEmpty();

        }
        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = _teamMemberEmail;

            var result = await DoPost(METHOD, request);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();

            var expctedMessage = ResourceErrorMessages.ResourceManager.GetString("EMAIL_ALREADY_REGISTERED");
            errorMessage.ShouldHaveSingleItem();
            errorMessage.ShouldContain(e => e.GetString()!.Equals(expctedMessage));

        }
    }

}
