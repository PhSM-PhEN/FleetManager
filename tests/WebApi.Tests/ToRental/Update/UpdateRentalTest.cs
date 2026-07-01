using System.Net;
using CommonTestUtilities.Request;
using Shouldly;

namespace WebApi.Tests.ToRental.Update
{
    public class UpdateRentalTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/Rental";
        private readonly string _teamMemberToken;
        private readonly long _rentalId;

        public UpdateRentalTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _rentalId = customWebApplication.RENTAL_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestUpdateRentJsonBuilder.Build();

            var result = await DoPut($"{METHOD}/{_rentalId}", request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestUpdateRentJsonBuilder.Build();

            var result = await DoPut($"{METHOD}/{_rentalId}", request);

            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Rental_Not_Found()
        {
            var request = RequestUpdateRentJsonBuilder.Build();

            var result = await DoPut($"{METHOD}/{100}", request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Error_StartDate_Must_Be_Less_Than_EndDate()
        {
            var request = RequestUpdateRentJsonBuilder.Build();
            request.StartDate = DateTime.UtcNow.AddDays(10);
            request.EndDate = DateTime.UtcNow.AddDays(5);

            var result = await DoPut($"{METHOD}/{_rentalId}", request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Error_EndDate_Must_Be_Greater_Than_CurrentDate()
        {
            var request = RequestUpdateRentJsonBuilder.Build();
            request.StartDate = DateTime.UtcNow.AddDays(-10);
            request.EndDate = DateTime.UtcNow.AddDays(-5);

            var result = await DoPut($"{METHOD}/{_rentalId}", request, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
