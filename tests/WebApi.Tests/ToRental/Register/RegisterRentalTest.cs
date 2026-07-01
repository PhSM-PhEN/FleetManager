using System.Net;
using CommonTestUtilities.Request;
using Shouldly;

namespace WebApi.Tests.ToRental.Register
{
    public class RegisterRentalTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Rental";
        private readonly string _teamMemberToken;
        private readonly long _companyId;
        private readonly long _clientId;
        private readonly long _vehicleAvailableId;
        private readonly long _vehicleWithActiveRentalId;
        private readonly long _rentalPlanId;

        public RegisterRentalTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _companyId = customWebApplication.COMPANY_TEAM_MEMBER.GetById();
            _clientId = customWebApplication.CLIENT_TEAM_MEMBER.GetById();
            _vehicleAvailableId = customWebApplication.VEHICLE_AVAILABLE_TEAM_MEMBER.GetById();
            _vehicleWithActiveRentalId = customWebApplication.VEHICLE_TEAM_MEMBER.GetById();
            _rentalPlanId = customWebApplication.RENTAL_PLAN_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestRentJsonBuilder.Build(_companyId, _clientId, _vehicleAvailableId, _rentalPlanId);

            var result = await DoPost(METHOD, request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestRentJsonBuilder.Build(_companyId, _clientId, _vehicleAvailableId, _rentalPlanId);

            var result = await DoPost(METHOD, request);

            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Client_Not_Found()
        {
            var request = RequestRentJsonBuilder.Build(_companyId, clientId: 100, _vehicleAvailableId, _rentalPlanId);

            var result = await DoPost(METHOD, request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var request = RequestRentJsonBuilder.Build(_companyId, _clientId, vehicleId: 100, _rentalPlanId);

            var result = await DoPost(METHOD, request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Company_Not_Found()
        {
            var request = RequestRentJsonBuilder.Build(companyId: 100, _clientId, _vehicleAvailableId, _rentalPlanId);

            var result = await DoPost(METHOD, request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_RentalPlan_Not_Found()
        {
            var request = RequestRentJsonBuilder.Build(_companyId, _clientId, _vehicleAvailableId, rentalPlanId: 100);

            var result = await DoPost(METHOD, request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_Vehicle_Already_Has_Active_Rental()
        {
           
            var request = RequestRentJsonBuilder.Build(_companyId, _clientId, _vehicleWithActiveRentalId, _rentalPlanId);

            var result = await DoPost(METHOD, request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Error_EndDate_Must_Be_Greater_Than_StartDate()
        {
            var request = RequestRentJsonBuilder.Build(_companyId, _clientId, _vehicleAvailableId, _rentalPlanId);
            request.StartDate = DateTime.UtcNow.AddDays(10);
            request.EndDate = DateTime.UtcNow.AddDays(5);

            var result = await DoPost(METHOD, request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
