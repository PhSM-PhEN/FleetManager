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
        private readonly string _token;
        public RegisterUserTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication) => _token = customWebApplication.USER_TEAM_MEMBER.GetToken();
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var result = await DoPost(METHOD, request);

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
            var result = await DoPost(METHOD, request, _token);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            
            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);
            var errorMessage = response.RootElement.GetProperty("errorMessage").EnumerateArray();

            var expctedMessage = ResourceErrorMessages.ResourceManager.GetString(ResourceErrorMessages.NAME_IS_REQUIRED);
            errorMessage.ShouldHaveSingleItem();
            errorMessage.ShouldContain(e => e.GetString()!.Equals(expctedMessage));
        }


    }
}
