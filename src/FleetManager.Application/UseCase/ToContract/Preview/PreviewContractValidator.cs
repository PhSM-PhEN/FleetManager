using FleetManager.Communication.Request.ToContract;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToContract.Preview
{
    public class PreviewContractValidator : AbstractValidator<RequestPreviewContractJson>
    {
        public PreviewContractValidator()
        {
            RuleFor(x => x.VehicleId).GreaterThan(0).WithMessage(ResourceErrorMessages.VEHICLE_ID_REQUIRED);
            RuleFor(x => x.TenantId).GreaterThan(0).WithMessage(ResourceErrorMessages.TENANT_ID_REQUIRED);
            RuleFor(x => x.DesiredExcessMileage).GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.ADDITIONAL_MILEAGE_MUST_BE_POSITIVE);

            RuleFor(x => x.RentalType)
                .Must(value => value == "Daily" || value == "Monthly")
                .WithMessage(ResourceErrorMessages.RENTAL_TYPE_INVALID);

            RuleFor(x => x.ReturnDueDateTime)
                .NotNull().WithMessage(ResourceErrorMessages.RETURN_DUE_DATE_REQUIRED)
                .When(x => x.RentalType == "Daily");

            RuleFor(x => x.ReturnDueDateTime)
                .Must((request, returnDue) => returnDue!.Value > request.PickupDateTime)
                .WithMessage(ResourceErrorMessages.RETURN_DUE_DATE_MUST_BE_AFTER_PICKUP)
                .When(x => x.RentalType == "Daily" && x.ReturnDueDateTime.HasValue);
        }
    }
}
