using CommonTestUtilities.Request.ToAddress;
using FleetManager.Application.UseCase.ToAddress;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.Address
{
    public class AddressValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new AddressValidator();
            var request = RequestAddressJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Street_Empty(string street)
        {
            var validator = new AddressValidator();
            var request = RequestAddressJsonBuilder.Build();
            request.Street = street;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.STREET_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Number_Empty(string number)
        {
            var validator = new AddressValidator();
            var request = RequestAddressJsonBuilder.Build();
            request.Number = number;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NUMBER_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_City_Empty(string city)
        {
            var validator = new AddressValidator();
            var request = RequestAddressJsonBuilder.Build();
            request.City = city;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.CITY_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_State_Empty(string state)
        {
            var validator = new AddressValidator();
            var request = RequestAddressJsonBuilder.Build();
            request.State = state;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.STATE_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_ZipCode_Empty(string zipCode)
        {
            var validator = new AddressValidator();
            var request = RequestAddressJsonBuilder.Build();
            request.ZipCode = zipCode;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.ZIPCODE_REQUIRED));
        }

        [Theory]
        [InlineData("1234-567")]
        [InlineData("abcde-123")]
        [InlineData("123456")]
        [InlineData("123456789")]
        public void Error_ZipCode_Invalid_Format(string zipCode)
        {
            var validator = new AddressValidator();
            var request = RequestAddressJsonBuilder.Build();
            request.ZipCode = zipCode;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.ZIPCODE_INVALID));
        }

        [Theory]
        [InlineData("12345-678")]
        [InlineData("12345678")]
        public void Success_ZipCode_With_Or_Without_Hyphen(string zipCode)
        {
            var validator = new AddressValidator();
            var request = RequestAddressJsonBuilder.Build();
            request.ZipCode = zipCode;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }
    }
}
