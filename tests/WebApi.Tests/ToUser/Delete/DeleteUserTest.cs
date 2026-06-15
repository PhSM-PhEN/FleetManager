using CommonTestUtilities.Request;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.ToUser.Delete
{
    public class DeleteUserTest : FleetManagerClassFixture
    {
        private readonly HttpClient _;
        private const string METHOD = "api/User";
        private readonly string _adminToken;
        private readonly string _teamMemberToken;

        public DeleteUserTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _ = factory.CreateClient();
            _adminToken = factory.USER_ADM_MEMBER.GetToken();
            _teamMemberToken = factory.USER_TEAM_MEMBER.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            // cria um usuário novo só para deletar
            var request = RequestRegisterUserJsonBuilder.Build();
            var registerResult = await DoPost(METHOD, request, _adminToken);
            var body = await registerResult.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);
            var token = responseBody.RootElement.GetProperty("token").GetString()!;

            // deleta com o token do usuário recém criado
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
