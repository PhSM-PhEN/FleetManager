using CommonTestUtilities.Entitie;

namespace WebApi.Tests.ToCompany.Get
{
    public class GetByIdCompanyTest : FleetManagerClassFixture
    {
        private const string MEHTOD = "api/Company";
        private readonly HttpClient _;
        private readonly string _userTeamMember;
        private readonly string _userAdmin;
        private readonly int _companyId;
        public GetByIdCompanyTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _ = customWebApplication.CreateClient();
            _userTeamMember = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _userAdmin = customWebApplication.USER_TEAM_MEMBER.GetToken();
          //  _companyId = customWebApplication.COMPANY_TEAM_MEMBER.
        }
        [Fact]
        public async Task Success_Admin_Token()
        {
            

            var result = await DoGet(MEHTOD,  _userAdmin);
        }
    }
}
