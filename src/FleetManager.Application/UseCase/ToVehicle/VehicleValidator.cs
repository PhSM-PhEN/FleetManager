using FleetManager.Communication.Request.ToVehicle;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToVehicle
{
    public class VehicleValidator : AbstractValidator<RequestVehicleJson>
    {
        public VehicleValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().WithMessage(ResourceErrorMessages.BRAND_REQUIRED);
            RuleFor(x => x.Model).NotEmpty().WithMessage(ResourceErrorMessages.MODEL_REQUIRED);
            RuleFor(x => x.Color).NotEmpty().WithMessage(ResourceErrorMessages.COLOR_REQUIRED);
            RuleFor(x => x.ManufacturingYear).NotEmpty().WithMessage(ResourceErrorMessages.MANUFACTURING_YEAR_REQUIRED);
            RuleFor(x => x.Renavam).NotEmpty().WithMessage(ResourceErrorMessages.RENAVAM_REQUIRED);
            RuleFor(x => x.ChassiNumber).NotEmpty().WithMessage(ResourceErrorMessages.CHASSI_NUMBER_REQUIRED);
            RuleFor(x => x.LicensePlate).NotEmpty().WithMessage(ResourceErrorMessages.LICENSE_PLATE_REQUIRED);
            RuleFor(x => x.CurrentMileage).GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.MILEAGE_INVALID);
            RuleFor(x => x.CompanyId).GreaterThan(0).WithMessage(ResourceErrorMessages.COMPANY_ID_REQUIRED);
        }
    }
}