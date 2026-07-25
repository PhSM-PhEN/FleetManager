using FleetManager.Communication.Request.ToVehicle;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToVehicle.Update
{
    public class CurrentMiliageValidator : AbstractValidator<RequestMileageVehicleJson>
    {
        public CurrentMiliageValidator()
        {
            RuleFor(v => v.MileageVehicle)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ResourceErrorMessages.MILEAGE_INVALID);
        }
    }
}
