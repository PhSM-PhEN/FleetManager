using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToContract;
using Shouldly;

namespace WebApi.Tests.ToContract.Delete
{
    public class DeleteContractUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Contract";
        private readonly string _adminToken;
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;
        private readonly long _tenantId;
        private readonly long _rentalPlanId;

        public DeleteContractUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _adminToken = customWebApplication.USER_ADM.GetToken();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _vehicleId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
            _tenantId = customWebApplication.TENANT_TEAM_MEMBER.GetById();
            _rentalPlanId = customWebApplication.RENTAL_PLAN_TEAM_MEMBER.GetById();
        }


    }
}
