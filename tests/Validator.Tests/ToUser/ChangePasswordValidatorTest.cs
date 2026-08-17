using CommonTestUtilities.Request.ToUser;
using FleetManager.Application.UseCase.ToUser.ChangePassword;
using Shouldly;

namespace Validator.Tests.ToUser
{
    public class ChangePasswordValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new ChangePasswordValidator();
            var request = RequestChangePasswordJsonBuilder.Build();

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
            var validator = new ChangePasswordValidator();
            var request = RequestChangePasswordJsonBuilder.Build();
            request.NewPassword = newPassword;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
        }
    }
}