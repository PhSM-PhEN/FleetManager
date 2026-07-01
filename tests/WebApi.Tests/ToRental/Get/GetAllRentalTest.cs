using System.Net;
using Shouldly;

namespace WebApi.Tests.ToRental.Get
{
    public class GetAllRentalTest : FleetManagerClassFixture
    {
        private readonly HttpClient _ ;
        private const string METHOD = "api/Rental";
        private readonly string _teamMemberToken;

        public GetAllRentalTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {   _ = customWebApplication.CreateClient();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoGet(METHOD, _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Success_With_Pagination()
        {
            var result = await DoGet($"{METHOD}?pageNumber=1&pageSize=1", _teamMemberToken);

            result.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var result = await DoGet(METHOD);

            result.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
