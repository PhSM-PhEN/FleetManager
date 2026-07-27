using CommonTestUtilities.Request.ToVehiclePricing;
using FleetManager.Application.UseCase.ToVehiclePricing;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.ToVehiclePricing
{
    public class VehiclePricingValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new VehiclePricingValidator();
            var request = RequestVehiclePricingJsonBuilder.Build(1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Error_VehicleId_Not_Greater_Than_Zero(long vehicleId)
        {
            var validator = new VehiclePricingValidator();
            var request = RequestVehiclePricingJsonBuilder.Build(vehicleId);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.VEHICLE_ID_REQUIRED));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Error_DailyPrice_Not_Greater_Than_Zero(decimal dailyPrice)
        {
            var validator = new VehiclePricingValidator();
            var request = RequestVehiclePricingJsonBuilder.Build(1);
            request.DailyPrice = dailyPrice;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.DAILY_PRICE_INVALID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Error_MonthlyPrice_Not_Greater_Than_Zero(decimal monthlyPrice)
        {
            var validator = new VehiclePricingValidator();
            var request = RequestVehiclePricingJsonBuilder.Build(1);
            request.MonthlyPrice = monthlyPrice;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.MONTHLY_PRICE_INVALID));
        }

        [Fact]
        public void Error_ExcessMileageRate_Negative()
        {
            var validator = new VehiclePricingValidator();
            var request = RequestVehiclePricingJsonBuilder.Build(1);
            request.ExcessMileageRate = -1;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXCESS_MILEAGE_RATE_INVALID));
        }

        [Fact]
        public void Success_ExcessMileageRate_Zero()
        {
            var validator = new VehiclePricingValidator();
            var request = RequestVehiclePricingJsonBuilder.Build(1);
            request.ExcessMileageRate = 0;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Error_MileagePerDay_Not_Greater_Than_Zero(long mileagePerDay)
        {
            var validator = new VehiclePricingValidator();
            var request = RequestVehiclePricingJsonBuilder.Build(1);
            request.MileagePerDay = mileagePerDay;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.MILEAGE_PER_DAY_INVALID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Error_MileagePerMonthly_Not_Greater_Than_Zero(long mileagePerMonthly)
        {
            var validator = new VehiclePricingValidator();
            var request = RequestVehiclePricingJsonBuilder.Build(1);
            request.MileagePerMonthly = mileagePerMonthly;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.MILEAGE_PER_MONTHLY_INVALID));
        }
    }
}
