using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToContract;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToContract.Preview
{
    public class PreviewContractUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Contract/Preview";
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;
        private readonly long _tenantId;

        public PreviewContractUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _vehicleId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
            _tenantId = customWebApplication.TENANT_TEAM_MEMBER.GetById();
        }


    }
}
