using Shouldly;
using System.Text.Json;

namespace WebApi.Tests.ToCompany.Get
{
    public class GetByIdCompanyTest : FleetManagerClassFixture
    {
        private const string MEHTOD = "api/Company";
        private readonly HttpClient _;
        private readonly string _userAdmin;
        private readonly long _companyId;
        public GetByIdCompanyTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _ = customWebApplication.CreateClient();
            _userAdmin = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _companyId = customWebApplication.COMPANY_TEAM_MEMBER.GetById();
        }
        [Fact]
        public async Task Success_Admin_Token()
        {


            var result = await DoGet(requestUri: $"{MEHTOD}/{_companyId}", _userAdmin);

            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();

            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("id").GetInt64().ShouldBe(_companyId);

        }
    }
}
