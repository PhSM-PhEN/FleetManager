using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToCompany;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToCompany.Delete
{
    public class DeleteCompanyUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Company";
        private readonly string _teamMemberToken;
        private readonly long _addressId;

        public DeleteCompanyUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _addressId = customWebApplication.ADDRESS_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestCompanyJsonBuilder.Build(_addressId);
            var registerResult = await DoPost(METHOD, request, _teamMemberToken);

            var body = await registerResult.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);
            var companyId = responseBody.RootElement.GetProperty("id").GetInt64();

            var result = await DoDelete($"{METHOD}/{companyId}", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Company_Not_Found()
        {
            var result = await DoDelete($"{METHOD}/0", _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoDelete($"{METHOD}/1");
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}