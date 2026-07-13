using CommonTestUtilities.Request.ToUser;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.ToUser.Delete
{
    public class DeleteUserTest : FleetManagerClassFixture
    {

        private const string METHOD = "api/User";
        private readonly string _teamMemberToken;

        public DeleteUserTest(CustomWebApplicationFactory factory) : base(factory)
        {
          
            _teamMemberToken = factory.USER_TEAM_MEMBER.GetToken();

        }

        [Fact]
        public async Task Success()
        {
            
            var request = RequestRegisterUserJsonBuilder.Build();
            var registerResult = await DoPost(METHOD, request, _teamMemberToken);
            var body = await registerResult.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);
            var token = responseBody.RootElement.GetProperty("token").GetString()!;

           
            var result = await DoDelete(METHOD, token);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoDelete(METHOD);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
