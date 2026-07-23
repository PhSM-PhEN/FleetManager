using CommonTestUtilities.Request.ToCompany;
using FleetManager.Application.UseCase.ToCompany;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.ToCompany
{
    public class CompanyValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new CompanyValidator();
            var request = RequestCompanyJsonBuilder.Build(1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Name_Empty(string name)
        {
            var validator = new CompanyValidator();
            var request = RequestCompanyJsonBuilder.Build(1);
            request.Name = name;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_IS_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Cnpj_Empty(string cnpj)
        {
            var validator = new CompanyValidator();
            var request = RequestCompanyJsonBuilder.Build(1);
            request.Cnpj = cnpj;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.CNPJ_REQUIRED));
        }

        [Theory]
        [InlineData("123")]
        [InlineData("abcdefghijklmnop")]
        [InlineData("11.111.111/1111-1")]
        public void Error_Cnpj_Invalid_Format(string cnpj)
        {
            var validator = new CompanyValidator();
            var request = RequestCompanyJsonBuilder.Build(1);
            request.Cnpj = cnpj;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.CNPJ_INVALID));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_PhoneNumber_Empty(string phoneNumber)
        {
            var validator = new CompanyValidator();
            var request = RequestCompanyJsonBuilder.Build(1);
            request.PhoneNumber = phoneNumber;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PHONE_NUMBER_REQUIRED));
        }

        [Fact]
        public void Error_AddressId_Not_Greater_Than_Zero()
        {
            var validator = new CompanyValidator();
            var request = RequestCompanyJsonBuilder.Build(0);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.ADDRESS_ID_REQUIRED));
        }
    }
}