using CommonTestUtilities.Request.ToUser;
using FleetManager.Application.UseCase.ToUser.ChangePassword;
using Shouldly;

namespace Validator.Tests.ToUser
{
    public class ChangPassworValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new ChangPassworValidator();
            var request = RequestChangPasswordJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("123456")]
        [InlineData("nouppercase1")]
        [InlineData("NOLOWERCASE1")]
        [InlineData("NoNumbersHere")]
        public void Error_NewPassword_Invalid(string newPassword)
        {
            var validator = new ChangPassworValidator();
            var request = RequestChangPasswordJsonBuilder.Build();
            request.NewPassword = newPassword;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
        }
    }
}