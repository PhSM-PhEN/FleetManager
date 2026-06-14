using FleetManager.Communication.Requests;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToVehicle
{
    public class VehicleUpdateValidator : AbstractValidator<RequestUpdateVehicleJson>
    {
        public VehicleUpdateValidator()
        {
           ;

            RuleFor(v => v.Model)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(ResourceErrorMessages.MODEL_REQUIRED);
  
            RuleFor(v => v.Brand)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.BRAND_REQUIRED);

            RuleFor(v => v.Color)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.COLOR_REQUIRED);


        }
    }
}
