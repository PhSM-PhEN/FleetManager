using CommonTestUtilities.Request.ToContract;
using FleetManager.Application.UseCase.ToContract;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.ToContract
{
    public class ContractValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new ContractValidator();
            var request = RequestContractJsonBuilder.Build(1, 1, 1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Success_Monthly_Without_ReturnDueDate()
        {
            var validator = new ContractValidator();
            var request = RequestContractJsonBuilder.Build(1, 1, 1, "Monthly");

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Error_VehicleId_Not_Greater_Than_Zero(long vehicleId)
        {
            var validator = new ContractValidator();
            var request = RequestContractJsonBuilder.Build(vehicleId, 1, 1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.VEHICLE_ID_REQUIRED));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Error_TenantId_Not_Greater_Than_Zero(long tenantId)
        {
            var validator = new ContractValidator();
            var request = RequestContractJsonBuilder.Build(1, tenantId, 1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TENANT_ID_REQUIRED));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Error_RentalPlanId_Not_Greater_Than_Zero(long rentalPlanId)
        {
            var validator = new ContractValidator();
            var request = RequestContractJsonBuilder.Build(1, 1, rentalPlanId);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.RENTAL_PLAN_ID_REQUIRED));
        }

        [Fact]
        public void Error_MileageContracted_Negative()
        {
            var validator = new ContractValidator();
            var request = RequestContractJsonBuilder.Build(1, 1, 1);
            request.MileageContracted = -1;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.MILEAGE_CONTRACTED_INVALID));
        }

        [Fact]
        public void Error_TotalAmount_Negative()
        {
            var validator = new ContractValidator();
            var request = RequestContractJsonBuilder.Build(1, 1, 1);
            request.TotalAmount = -1;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TOTAL_AMOUNT_INVALID));
        }

        [Theory]
        [InlineData("")]
        [InlineData("Weekly")]
        [InlineData("Yearly")]
        public void Error_RentalType_Invalid(string rentalType)
        {
            var validator = new ContractValidator();
            var request = RequestContractJsonBuilder.Build(1, 1, 1);
            request.RentalType = rentalType;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.RENTAL_TYPE_INVALID));
        }

        [Fact]
        public void Error_ReturnDueDate_Required_When_Daily()
        {
            var validator = new ContractValidator();
            var request = RequestContractJsonBuilder.Build(1, 1, 1, "Daily");
            request.ReturnDueDateTime = null;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.RETURN_DUE_DATE_REQUIRED));
        }

        [Fact]
        public void Error_ReturnDueDate_Must_Be_After_Pickup()
        {
            var validator = new ContractValidator();
            var request = RequestContractJsonBuilder.Build(1, 1, 1, "Daily");
            request.ReturnDueDateTime = request.PickupDateTime.AddDays(-1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.RETURN_DUE_DATE_MUST_BE_AFTER_PICKUP));
        }
    }
}
