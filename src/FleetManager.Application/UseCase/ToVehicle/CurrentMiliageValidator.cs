using FleetManager.communication.Requests.ToVehicle;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToVehicle
{
    public class CurrentMiliageValidator : AbstractValidator<RequestVehicleUpdateCurrentMiliageJson>
    {
        public CurrentMiliageValidator() 
        {
            RuleFor(v => v.CurrentMileage)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ResourceErrorMessages.CURRENT_MILEAGE_EMPTY);
        }
    }
}
