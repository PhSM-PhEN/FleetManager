using System.Net;
using System.Text.Json;
using CommonTestUtilities.Entitie;
using CommonTestUtilities.Request;
using FleetManager.Exception.ExceptionBase;
using Shouldly;
using WebApi.Tests.Resource;

namespace WebApi.Tests.ToCompany.Register
{
    public class RegisterCompanyTest : FleetManagerClassFixture
    {
        private readonly HttpClient _;
        private const string METHOD = "api/Company";
        private readonly string _adminToken;
      
  
        public RegisterCompanyTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _ = customWebApplication.CreateClient();
            _adminToken = customWebApplication.USER_ADM_MEMBER.GetToken();
        }
        [Fact]
        public async Task Success()
        {   
            
            var request =  RequestCompanyJsonBuilder.Build();

            var result = await DoPost(METHOD, request, _adminToken);

            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);
            responseBody.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
            responseBody.RootElement.GetProperty("cnpj").GetString().ShouldBe(request.Cnpj);
            responseBody.RootElement.GetProperty("address").GetProperty("street").GetString().ShouldNotBeNullOrWhiteSpace();
        }
        [Fact]
        public async Task Error_Without_Token()
        {
            var request = CompanyBuilder.Build();
            var result = await DoPost(METHOD, request);
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestCompanyJsonBuilder.Build();
            request.Name = string.Empty;
            var result = await DoPost(METHOD, request, _adminToken);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);
            
            var erros = response.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_IS_REQUIRED");

            erros.ShouldContain(x => x.GetString()!.Equals(expectedMessage));

        }

    }

}


