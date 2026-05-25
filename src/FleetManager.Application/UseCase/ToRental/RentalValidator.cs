using FleetManager.communication.Requests;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToRental
{
    public class RentalValidator : AbstractValidator<RequestRentJson>
    {
        public RentalValidator()
        {
            RuleFor(rent => rent.CompanyId)
             .GreaterThan(0)
             .WithMessage(ResourceErrorMessages.COMPANYID_IS_REQUIRED);

            RuleFor(rent => rent.ClientId)
                .GreaterThan(0)
                .WithMessage(ResourceErrorMessages.CLIENT_IS_REQUIRED);

            RuleFor(rent => rent.VehicleId)
                .GreaterThan(0)
                .WithMessage(ResourceErrorMessages.VEHICLEID_IS_REQUIRED);

            RuleFor(rent => rent.TotalPrice)
                .GreaterThan(0)
                .WithMessage(ResourceErrorMessages.TOTAL_PRICE_MUST_BE_GREATER_THAN_ZERO);

            RuleFor(rent => rent.StartDate)
                .LessThan(rent => rent.EndDate)
                .WithMessage(ResourceErrorMessages.START_DATE_MUST_BE_LESS_THAN_END_DATE);

            RuleFor(rent => rent.EndDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage(ResourceErrorMessages.END_DATE_MUST_BE_GREATER_THAN_CURRENT_DATE);
        }
    }
}
