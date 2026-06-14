using FleetManager.Communication.Requests;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Tests.DoLogin
{
    public class DoLoginTest(CustomWebApplicationFactory webApplicationFactory) : IClassFixture<CustomWebApplicationFactory>
    {
        private const string METHOD = "api/login";
        private readonly HttpClient _httpClient = webApplicationFactory.CreateClient();
        private readonly string _email = webApplicationFactory.USER_TEAM_MEMBER.GetEmail();
        private readonly string _name = webApplicationFactory.USER_TEAM_MEMBER.GetName();
        private readonly string _password = webApplicationFactory.USER_TEAM_MEMBER.GetPassword();

        [Fact]
        public async Task Success()
        {
            var request = new RequestLoginUserJson
            {
                Email = _email,
                Password = _password
            };
            var response = await _httpClient.PostAsJsonAsync(METHOD, request);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("name").GetString().ShouldBe(_name);
            responseData.RootElement.GetProperty("token").GetString().ShouldNotBeNullOrEmpty();
        }
        [Fact]
        public async Task Error_Wrong_Password()
        {
            var request = new RequestLoginUserJson
            {
                Email = _email,
                Password = "wrongPassword123"
            };
            var response = await _httpClient.PostAsJsonAsync(METHOD, request);
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Email_Not_Found()
        {
            var request = new RequestLoginUserJson
            {
                Email = "notexists@email.com",
                Password = _password
            };
            var response = await _httpClient.PostAsJsonAsync(METHOD, request);
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

    }
}
