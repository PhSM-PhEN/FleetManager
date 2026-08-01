using CommonTestUtilities.Request.ToContract;
using FleetManager.Application.UseCase.ToContract.Update;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.ToContract
{
    public class UpdateContractValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new UpdateContractValidator();
            var request = RequestUpdateContractJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Success_Monthly_Without_ReturnDueDate()
        {
            var validator = new UpdateContractValidator();
            var request = RequestUpdateContractJsonBuilder.Build("Monthly");

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Error_MileageContracted_Negative()
        {
            var validator = new UpdateContractValidator();
            var request = RequestUpdateContractJsonBuilder.Build();
            request.MileageContracted = -1;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.MILEAGE_CONTRACTED_INVALID));
        }

        [Fact]
        public void Error_TotalAmount_Negative()
        {
            var validator = new UpdateContractValidator();
            var request = RequestUpdateContractJsonBuilder.Build();
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
            var validator = new UpdateContractValidator();
            var request = RequestUpdateContractJsonBuilder.Build();
            request.RentalType = rentalType;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.RENTAL_TYPE_INVALID));
        }

        [Fact]
        public void Error_ReturnDueDate_Required_When_Daily()
        {
            var validator = new UpdateContractValidator();
            var request = RequestUpdateContractJsonBuilder.Build("Daily");
            request.ReturnDueDateTime = null;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.RETURN_DUE_DATE_REQUIRED));
        }

        [Fact]
        public void Error_ReturnDueDate_Must_Be_After_Pickup()
        {
            var validator = new UpdateContractValidator();
            var request = RequestUpdateContractJsonBuilder.Build("Daily");
            request.ReturnDueDateTime = request.PickupDateTime.AddDays(-1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.RETURN_DUE_DATE_MUST_BE_AFTER_PICKUP));
        }
    }
}
