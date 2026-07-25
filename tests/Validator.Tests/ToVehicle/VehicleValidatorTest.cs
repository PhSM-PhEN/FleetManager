using CommonTestUtilities.Request.ToVehicle;
using FleetManager.Application.UseCase.ToVehicle;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.ToVehicle
{
    public class VehicleValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new VehicleValidator();
            var request = RequestVehicleJsonBuilder.Build(1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Brand_Empty(string brand)
        {
            var validator = new VehicleValidator();
            var request = RequestVehicleJsonBuilder.Build(1);
            request.Brand = brand;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.BRAND_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Model_Empty(string model)
        {
            var validator = new VehicleValidator();
            var request = RequestVehicleJsonBuilder.Build(1);
            request.Model = model;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.MODEL_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Color_Empty(string color)
        {
            var validator = new VehicleValidator();
            var request = RequestVehicleJsonBuilder.Build(1);
            request.Color = color;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.COLOR_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_ManufacturingYear_Empty(string manufacturingYear)
        {
            var validator = new VehicleValidator();
            var request = RequestVehicleJsonBuilder.Build(1);
            request.ManufacturingYear = manufacturingYear;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.MANUFACTURING_YEAR_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Renavam_Empty(string renavam)
        {
            var validator = new VehicleValidator();
            var request = RequestVehicleJsonBuilder.Build(1);
            request.Renavam = renavam;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.RENAVAM_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_ChassiNumber_Empty(string chassiNumber)
        {
            var validator = new VehicleValidator();
            var request = RequestVehicleJsonBuilder.Build(1);
            request.ChassiNumber = chassiNumber;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.CHASSI_NUMBER_REQUIRED));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_LicensePlate_Empty(string licensePlate)
        {
            var validator = new VehicleValidator();
            var request = RequestVehicleJsonBuilder.Build(1);
            request.LicensePlate = licensePlate;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.LICENSE_PLATE_REQUIRED));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-1000)]
        public void Error_CurrentMileage_Negative(long currentMileage)
        {
            var validator = new VehicleValidator();
            var request = RequestVehicleJsonBuilder.Build(1);
            request.CurrentMileage = currentMileage;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.MILEAGE_INVALID));
        }

        [Fact]
        public void Success_CurrentMileage_Zero()
        {
            var validator = new VehicleValidator();
            var request = RequestVehicleJsonBuilder.Build(1);
            request.CurrentMileage = 0;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Error_CompanyId_Not_Greater_Than_Zero(long companyId)
        {
            var validator = new VehicleValidator();
            var request = RequestVehicleJsonBuilder.Build(companyId);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.COMPANY_ID_REQUIRED));
        }
    }
}