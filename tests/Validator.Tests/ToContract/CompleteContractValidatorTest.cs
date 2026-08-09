using CommonTestUtilities.Request.ToContract;
using FleetManager.Application.UseCase.ToContract.Complete;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.ToContract
{
    public class CompleteContractValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new CompleteContractValidator();
            var request = RequestCompleteContractJsonBuilder.Build(1000);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Error_FinalMileage_Negative()
        {
            var validator = new CompleteContractValidator();
            var request = RequestCompleteContractJsonBuilder.Build(-1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.MILEAGE_INVALID));
        }
    }
}
