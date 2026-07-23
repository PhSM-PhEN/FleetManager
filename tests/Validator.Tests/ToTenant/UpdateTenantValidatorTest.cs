using CommonTestUtilities.Request.ToTenant;
using FleetManager.Application.UseCase.ToTenant.Update;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.ToTenant
{
    public class UpdateTenantValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new UpdateTenantValidator();
            var request = RequestUpdateTenantJsonBuilder.Build(1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_PhoneNumber_Empty(string phoneNumber)
        {
            var validator = new UpdateTenantValidator();
            var request = RequestUpdateTenantJsonBuilder.Build(1);
            request.PhoneNumber = phoneNumber;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PHONE_NUMBER_REQUIRED));
        }

        [Fact]
        public void Error_Email_Invalid_When_Provided()
        {
            var validator = new UpdateTenantValidator();
            var request = RequestUpdateTenantJsonBuilder.Build(1);
            request.Email = "not-an-email";

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_INVALID));
        }

        [Fact]
        public void Error_AddressId_Not_Greater_Than_Zero()
        {
            var validator = new UpdateTenantValidator();
            var request = RequestUpdateTenantJsonBuilder.Build(0);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.ADDRESS_ID_REQUIRED));
        }
    }
}