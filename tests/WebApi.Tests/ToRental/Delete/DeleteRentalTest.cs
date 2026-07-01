using Shouldly;
using System.Net;

namespace WebApi.Tests.ToRental.Delete
{
    public class DeleteRentalTest : FleetManagerClassFixture
    {
        private readonly HttpClient _;
        private const string METHOD = "api/Rental";
        private readonly string _teamMemberToken;
        private readonly long _rentalId;

        public DeleteRentalTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _ = customWebApplication.CreateClient();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _rentalId = customWebApplication.RENTAL_TEAM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoDelete($"{METHOD}/{_rentalId}", _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoDelete($"{METHOD}/{_rentalId}");

            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Rental_Not_Found()
        {
            var result = await DoDelete($"{METHOD}/{100}", _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
