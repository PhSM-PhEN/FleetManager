using Shouldly;
using System.Net;

namespace WebApi.Tests.ToCompany.Delete
{
    public class DeleteCompanyTest : FleetManagerClassFixture
    {
        private readonly HttpClient _;
        private const string METHOD = "api/Company";
        private readonly string _teamMemberToken;
        private readonly long _companyId;

        public DeleteCompanyTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _ = customWebApplication.CreateClient();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _companyId = customWebApplication.COMPANY_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoDelete($"{METHOD}/{_companyId}", _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoDelete($"{METHOD}/{_companyId}");

            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Company_Not_Found()
        {
            var result = await DoDelete($"{METHOD}/0", _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}