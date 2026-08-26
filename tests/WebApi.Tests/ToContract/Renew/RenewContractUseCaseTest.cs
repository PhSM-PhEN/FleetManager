using System.Net;
using CommonTestUtilities.Request.ToContract;
using Shouldly;

namespace WebApi.Tests.ToContract.Renew
{
    public class RenewContractUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Contract";
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;
        private readonly long _tenantId;
        private readonly long _rentalPlanId;
        private readonly long _seededContractId;

        public RenewContractUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _vehicleId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
            _tenantId = customWebApplication.TENANT_TEAM_MEMBER.GetById();
            _rentalPlanId = customWebApplication.RENTAL_PLAN_TEAM_MEMBER.GetById();
            _seededContractId = customWebApplication.CONTRACT_TEAM_MEMBER.GetById();
        }

    }
}
