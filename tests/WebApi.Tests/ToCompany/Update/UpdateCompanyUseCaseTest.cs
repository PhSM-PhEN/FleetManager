using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToCompany;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToCompany.Update
{
    public class UpdateCompanyUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Company";
        private readonly string _teamMemberToken;
        private readonly long _addressId;

        public UpdateCompanyUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _addressId = customWebApplication.ADDRESS_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var companyId = await RegisterCompany();
            var request = RequestCompanyJsonBuilder.Build(_addressId);

            var result = await DoPut($"{METHOD}/{companyId}", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Company_Not_Found()
        {
            var request = RequestCompanyJsonBuilder.Build(_addressId);

            var result = await DoPut($"{METHOD}/0", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var companyId = await RegisterCompany();
            var request = RequestCompanyJsonBuilder.Build(_addressId);
            request.Name = string.Empty;

            var result = await DoPut($"{METHOD}/{companyId}", request, _teamMemberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestCompanyJsonBuilder.Build(_addressId);

            var result = await DoPut($"{METHOD}/1", request);
            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        private async Task<long> RegisterCompany()
        {
            var request = RequestCompanyJsonBuilder.Build(_addressId);
            var registerResult = await DoPost(METHOD, request, _teamMemberToken);

            var body = await registerResult.Content.ReadAsStreamAsync();
            var responseBody = await JsonDocument.ParseAsync(body);

            return responseBody.RootElement.GetProperty("id").GetInt64();
        }
    }
}