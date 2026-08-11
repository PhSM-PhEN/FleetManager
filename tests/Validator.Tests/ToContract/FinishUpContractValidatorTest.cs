using CommonTestUtilities.Request.ToContract;
using FleetManager.Application.UseCase.ToContract.FinishUp;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.ToContract
{
    public class FinishUpContractValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new FinishUpContractValidator();
            var request = RequestFinishUpContractJsonBuilder.Build(1000);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Error_FinalMileage_Negative()
        {
            var validator = new FinishUpContractValidator();
            var request = RequestFinishUpContractJsonBuilder.Build(-1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.MILEAGE_INVALID));
        }
    }
}
