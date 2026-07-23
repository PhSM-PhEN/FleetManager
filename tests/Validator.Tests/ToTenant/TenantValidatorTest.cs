using CommonTestUtilities.Request.ToTenant;
using FleetManager.Application.UseCase.ToTenant;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.ToTenant
{
    public class TenantValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new TenantValidator();
            var request = RequestTenantJsonBuilder.Build(1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Name_Empty(string name)
        {
            var validator = new TenantValidator();
            var request = RequestTenantJsonBuilder.Build(1);
            request.Name = name;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_IS_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Cpf_Empty(string cpf)
        {
            var validator = new TenantValidator();
            var request = RequestTenantJsonBuilder.Build(1);
            request.Cpf = cpf;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.CPF_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Rg_Empty(string rg)
        {
            var validator = new TenantValidator();
            var request = RequestTenantJsonBuilder.Build(1);
            request.Rg = rg;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.RG_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_DriverLicenseNumber_Empty(string number)
        {
            var validator = new TenantValidator();
            var request = RequestTenantJsonBuilder.Build(1);
            request.DriverLicenseNumber = number;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.DRIVER_LICENSE_NUMBER_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_DriverLicenseCategory_Empty(string category)
        {
            var validator = new TenantValidator();
            var request = RequestTenantJsonBuilder.Build(1);
            request.DriverLicenseCategory = category;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.DRIVER_LICENSE_CATEGORY_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_PhoneNumber_Empty(string phoneNumber)
        {
            var validator = new TenantValidator();
            var request = RequestTenantJsonBuilder.Build(1);
            request.PhoneNumber = phoneNumber;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PHONE_NUMBER_REQUIRED));
        }

        [Fact]
        public void Error_Email_Invalid_When_Provided()
        {
            var validator = new TenantValidator();
            var request = RequestTenantJsonBuilder.Build(1);
            request.Email = "not-an-email";

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_INVALID));
        }

        [Fact]
        public void Success_Email_Null_Is_Allowed()
        {
            var validator = new TenantValidator();
            var request = RequestTenantJsonBuilder.Build(1);
            request.Email = null;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Error_AddressId_Not_Greater_Than_Zero()
        {
            var validator = new TenantValidator();
            var request = RequestTenantJsonBuilder.Build(0);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.ADDRESS_ID_REQUIRED));
        }
    }
}