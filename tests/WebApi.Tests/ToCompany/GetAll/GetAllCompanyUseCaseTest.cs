using System.Net;
using System.Text.Json;
using Shouldly;

namespace WebApi.Tests.ToCompany.GetAll
{
    public class GetAllCompanyUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Company";
        private readonly string _adminToken;
        private readonly string _teamMemberToken;

        public GetAllCompanyUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _adminToken = customWebApplication.USER_ADM.GetToken();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
        }


    }
}
