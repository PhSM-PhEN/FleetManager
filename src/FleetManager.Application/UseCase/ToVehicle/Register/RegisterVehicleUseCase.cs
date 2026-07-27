using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToVehicle;
using FleetManager.Communication.Response.ToVehicle;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Entities.ValueObjects;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Domain.Repositories.ToRentalPlan;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Register
{
    public class RegisterVehicleUseCase(IVehicleWriteOnlyRepository repository, ICompanyReadOnlyRepository companyRepository, IRentalPlanReadOnlyRepository rentalPlanRepository, IUnitOfWork unitOfWork) : IRegisterVehicleUseCase
    {
        public async Task<ResponseShortVehicleJson> Execute(RequestVehicleJson request)
        {
            Validate(request);
            _ = await companyRepository.GetById(request.CompanyId) ??
                throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);
            _ = await rentalPlanRepository.GetById(request.VehiclePricingId) ??
                throw new NotFoundException(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);
            var manufacturingYear = ManufacturingYear.Parse(request.ManufacturingYear);

            var vehicle = new Vehicle(
                request.Brand,
                request.Model,
                request.Color,
                manufacturingYear,
                new Renavam(request.Renavam),
                new ChassiNumber(request.ChassiNumber),
                new LicensePlate(request.LicensePlate),
                request.CurrentMileage,
                request.CompanyId,
                request.VehiclePricingId
            );

            await repository.Add(vehicle);
            await unitOfWork.Commit();

            return vehicle.ToResponse();
        }
        private static void Validate(RequestVehicleJson request)
        {
            var validator = new VehicleValidator();
            var result = validator.Validate(request);
            if(result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
