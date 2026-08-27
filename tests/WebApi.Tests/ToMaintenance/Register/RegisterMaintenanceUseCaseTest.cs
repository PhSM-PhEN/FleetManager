using System.Net;
using System.Text.Json;
using CommonTestUtilities.Request.ToMaintenance;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace WebApi.Tests.ToMaintenance.Register
{
    public class RegisterMaintenanceUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Maintenance";
        private readonly string _teamMemberToken;
        private readonly long _vehicleId;

        public RegisterMaintenanceUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _vehicleId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
        }

 
    }
}
