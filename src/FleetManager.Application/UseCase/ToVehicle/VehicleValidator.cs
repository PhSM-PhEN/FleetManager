using FleetManager.communication.Requests;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToVehicle
{
    public class VehicleValidator : AbstractValidator<RequestVehicleJson>
    {

        public VehicleValidator()
        {

            RuleFor(v => v.LicensePlate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(ResourceErrorMessages.LICENSE_PLATE_REQUIRED)
                .Matches(@"^[A-Z]{3}[0-9]{4}$|^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$")
                    .WithMessage(ResourceErrorMessages.LICENSE_PLATE_INVALID_FORMAT);

            RuleFor(v => v.Model)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(ResourceErrorMessages.MODEL_REQUIRED);   

            RuleFor(v => v.Renavam)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(ResourceErrorMessages.RENAVAM_REQUIRED)
                .Length(11)
                    .WithMessage(ResourceErrorMessages.INVALID_LENGTH);

            RuleFor(v => v.Brand)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.BRAND_REQUIRED);

            RuleFor(v => v.ManufacturingYear)
                .GreaterThanOrEqualTo(2014)
                .WithMessage(ResourceErrorMessages.MANUFACTURE_MUST_BE_GREATER_THAN_OR_EQUAL_TO_2010);

            RuleFor(v => v.Color)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.COLOR_REQUIRED);

            RuleFor(v => v.FuelType)
                .IsInEnum()
                .WithMessage(ResourceErrorMessages.FUEL_TYPE_INVALID);

            RuleFor(v => v.TransmissionType)
                .IsInEnum()
                .WithMessage(ResourceErrorMessages.TRANSMISSION_TYPE_INVALID);

            RuleFor(v => v.CurrentMileage)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ResourceErrorMessages.CURRENT_MILEAGE_EMPTY);
            RuleFor(v => v.CategoryId)
                .GreaterThan(0)
                .WithMessage(ResourceErrorMessages.CATEGORY_ID_INVALID);
        }

    }
}
