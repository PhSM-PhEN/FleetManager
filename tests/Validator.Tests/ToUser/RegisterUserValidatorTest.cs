using CommonTestUtilities.Request.ToUser;
using FleetManager.Application.UseCase.ToUser;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.ToUser
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new UserValidator();
            var user = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(user);

            result.IsValid.ShouldBeTrue();

        }
        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Name_Invalid(string name)
        {
            var validator = new UserValidator();
            var user = RequestRegisterUserJsonBuilder.Build();
            user.Name = name;

            var result = validator.Validate(user);

            result.IsValid.ShouldBeFalse();

            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_IS_REQUIRED));
        }
        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Email_Empty(string email)
        {
            var validator = new UserValidator();
            var user = RequestRegisterUserJsonBuilder.Build();
            user.Email = email;

            var result = validator.Validate(user);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_IS_REQUIRED));

        }
        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = new UserValidator();
            var user = RequestRegisterUserJsonBuilder.Build();
            user.Email = "email.com";

            var result = validator.Validate(user);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_EMAIL_FORMAT));
        }
        [Fact]
        public void Error_Password_Invalid()
        {
            var validator = new UserValidator();
            var user = RequestRegisterUserJsonBuilder.Build();
            user.Password = "1234567";

            var result = validator.Validate(user);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.THE_PASSWORD_MUST_BE_MORE_THAN_8_CHARACTERS));
        }

    }
}
