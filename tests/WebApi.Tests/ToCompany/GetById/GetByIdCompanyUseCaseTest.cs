using System.Net;
using System.Text.Json;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToCompany.GetById
{
    public class GetByIdCompanyUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Company";
        private readonly string _teamMemberToken;
        private readonly long _companyId;

        public GetByIdCompanyUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _companyId = customWebApplication.COMPANY_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoGet($"{METHOD}/{_companyId}", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            responseBody.RootElement.GetProperty("id").GetInt64().ShouldBe(_companyId);
        }

        [Fact]
        public async Task Error_Company_Not_Found()
        {
            var result = await DoGet($"{METHOD}/0", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);

            var body = await result.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            var errorMessage = responseBody.RootElement.GetProperty("errorMessage").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("COMPANY_NOT_FOUND");

            errorMessage.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoGet($"{METHOD}/{_companyId}");
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}