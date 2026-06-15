using CommonTestUtilities.Request;
using FleetManager.Communication.Requests;
using Shouldly;
using System.Net;

namespace WebApi.Tests.ToUser.ChangePassword
{
    public class ChangePasswordTest : FleetManagerClassFixture
    {
        private const string METHOD = "api/User/ChangePassword";
        private readonly string _token;
        private readonly string _password;
        private readonly string _email;

        public ChangePasswordTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _token = customWebApplication.USER_TEAM_MEMBER.GetToken();
            _password = customWebApplication.USER_TEAM_MEMBER.GetPassword();
            _email = customWebApplication.USER_TEAM_MEMBER.GetEmail();
        }
        [Fact]
        public async Task Success()
        {
            var request = RequestChangPasswordJsonBuilder.Build();
            request.Password = _password;

            var response = await DoPut(METHOD, request: request, token: _token);
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);


            var loginRequest = new RequestLoginUserJson
            {
                Email = _email,
                Password = _password,
            };


            response = await DoPost("api/login", loginRequest);
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

            loginRequest.Password = request.NewPassword;

            response = await DoPost("api/login", loginRequest);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
