using System.Net;
using System.Text.Json;
using FleetManager.Communication.Request.ToUser;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToLogin
{
    public class LoginTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Login";
        private readonly string _email;
        private readonly string _password;

        public LoginTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _email = customWebApplication.USER_TEAM_MEMBER.GetEmail();
            _password = customWebApplication.USER_TEAM_MEMBER.GetPassword();
        }

        [Fact]
        public async Task Success()
        {
            var request = new RequestLoginUserJson { Email = _email, Password = _password };

            var result = await DoPost(METHOD, request);
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("token").GetString().ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async Task Error_Wrong_Password()
        {
            var request = new RequestLoginUserJson { Email = _email, Password = "wrongPassword1A" };

            var result = await DoPost(METHOD, request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Email_Not_Found()
        {
            var request = new RequestLoginUserJson { Email = "notfound@fleetmanager.com", Password = _password };

            var result = await DoPost(METHOD, request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}