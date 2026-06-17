using System.Text.Json;
using CommonTestUtilities.Request;
using Shouldly;

namespace WebApi.Tests.ToCompany.Register
{
    public class RegisterCompanyTest : FleetManagerClassFixture
    {
        private readonly HttpClient httpClient;
        private const string METHOD = "api/Company";
        private readonly string _teamMemberToken;
        private readonly string _adminTokem;
        public RegisterCompanyTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            httpClient = customWebApplication.CreateClient();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _adminTokem = customWebApplication.USER_ADM_MEMBER.GetToken();

        }
        [Fact]
        public async Task Success()
        {   
            
            var request =  RequestCompanyJsonBuilder.Build(1);
            var result = await DoPost(METHOD, request, _teamMemberToken);

            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);
            responseBody.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
            responseBody.RootElement.GetProperty("cnpj").GetString().ShouldBe(request.Cnpj);
        }
        

    }

}


