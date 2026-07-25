using FleetManager.Application.UseCase.ToVehicle.Update;
using FleetManager.Communication.Request.ToVehicle;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.ToVehicle
{
    public class CurrentMiliageValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new CurrentMiliageValidator();
            var request = new RequestMileageVehicleJson { MileageVehicle = 50_000 };

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Success_Mileage_Zero()
        {
            var validator = new CurrentMiliageValidator();
            var request = new RequestMileageVehicleJson { MileageVehicle = 0 };

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-1000)]
        public void Error_Mileage_Negative(long mileage)
        {
            var validator = new CurrentMiliageValidator();
            var request = new RequestMileageVehicleJson { MileageVehicle = mileage };

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.MILEAGE_INVALID));
        }
    }
}