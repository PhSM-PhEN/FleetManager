using CommonTestUtilities.Request.ToUser;
using FleetManager.Communication.Request.ToUser;
using Shouldly;

namespace WebApi.Tests.ToUser1
{
    public class RegisterUserUseCaseTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/User";
        //private readonly HttpClient client;
        private readonly string _teamMemberToken;
        private readonly string _teamMemberEmail;
        public RegisterUserUseCaseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            //client = customWebApplication.CreateClient();
            _teamMemberToken = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _teamMemberEmail = customWebApplication.USER_TEAM_MEMBER.GetEmail();
            
        }
        [Fact]
        public async Task Success()
        {
            // esta faltando o settings para o test 
            var request = RequestRegisterUserJsonBuilder.Build();

            var result = await DoPost(METHOD, request, _teamMemberToken);
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);
        }
    }
}
