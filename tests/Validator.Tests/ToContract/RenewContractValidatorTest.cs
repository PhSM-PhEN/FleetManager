using CommonTestUtilities.Request.ToContract;
using FleetManager.Application.UseCase.ToContract.Renew;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.ToContract
{
    public class RenewContractValidatorTest
    {
        [Fact]
        public void Success_No_Overrides()
        {
            var validator = new RenewContractValidator();
            var request = RequestRenewContractJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Success_With_Overrides()
        {
            var validator = new RenewContractValidator();
            var request = RequestRenewContractJsonBuilder.Build(newRentalPlanId: 1, mileageContracted: 1000);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Error_MileageContracted_Negative()
        {
            var validator = new RenewContractValidator();
            var request = RequestRenewContractJsonBuilder.Build(mileageContracted: -1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.MILEAGE_CONTRACTED_INVALID));
        }

        [Fact]
        public void Error_NewRentalPlanId_Zero()
        {
            var validator = new RenewContractValidator();
            var request = RequestRenewContractJsonBuilder.Build(newRentalPlanId: 0);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.RENTAL_PLAN_ID_REQUIRED));
        }
    }
}
