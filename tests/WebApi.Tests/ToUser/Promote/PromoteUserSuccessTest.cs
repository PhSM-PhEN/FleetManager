using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToUser;
using Shouldly;

namespace WebApi.Tests.ToUser.Promote
{
    public class PromoteUserSuccessTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/User/Promote";
        private readonly string _adminToken;

        public PromoteUserSuccessTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _adminToken = customWebApplication.USER_ADM.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            var deleteResult = await DoDelete("api/User", _adminToken);
            deleteResult.StatusCode.ShouldBe(HttpStatusCode.NoContent);

            var registerRequest = RequestRegisterUserJsonBuilder.Build();
            var registerResult = await DoPost("api/User", registerRequest);
            var body = await registerResult.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);
            var newUserToken = responseBody.RootElement.GetProperty("token").GetString()!;

            var promoteResult = await DoPatch(METHOD, newUserToken);
            promoteResult.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}