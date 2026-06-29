using System.Net;
using CommonTestUtilities.Request;
using Shouldly;

namespace WebApi.Tests.ToCompany.Update
{
    public class CompanyUpdateTest : FleetManagerClassFixture
    {
        private readonly HttpClient _;
        private const string METHOD = "api/Company";
        private readonly string _teamMemberToken;
        private readonly long _addressId;
        public CompanyUpdateTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _ = customWebApplication.CreateClient();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _addressId = customWebApplication.ADDRESS_TEAM_MEMBER.GetById();
            
        }
        [Fact]
        public async Task Success()
        {
          
            var request = RequestUpdateCompanyBuild.Build();
            var result = await DoPut($"{METHOD}/{_addressId}", request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        }
    }
}
