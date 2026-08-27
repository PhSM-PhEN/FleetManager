using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToMaintenance;
using Shouldly;

namespace WebApi.Tests.ToMaintenance.Delete
{
    public class DeleteMaintenanceUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Maintenance";
        private readonly string _adminToken;
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;

        public DeleteMaintenanceUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _adminToken = customWebApplication.USER_ADM.GetToken();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _vehicleId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
        }


    }
}
