using System.Net;
using Shouldly;

namespace WebApi.Tests.ToRental.Get
{
    public class GetByIdRentalTest : FleetManagerClassFixture
    {
        private readonly HttpClient _;
        private const string METHOD = "api/Rental";
        private readonly string _teamMemberToken;
        private readonly long _rentalId;

        public GetByIdRentalTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _ = customWebApplication.CreateClient();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _rentalId = customWebApplication.RENTAL_ADM_MEMBER.GetById();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoGet($"{METHOD}/{_rentalId}", _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoGet($"{METHOD}/{_rentalId}");

            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Rental_Not_Found()
        {
            var result = await DoGet($"{METHOD}/{100}", _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
