using System.Net;
using System.Text.Json;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToCompany.GetById
{
    public class GetByIdCompanyUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Company";
        private readonly string _adminToken;
        private readonly string _teamMemberToken;
        private readonly long _companyId;

        public GetByIdCompanyUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _adminToken = customWebApplication.USER_ADM.GetToken();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _companyId = customWebApplication.COMPANY_TEAM_MEMBER.GetById();
        }


    }
}
