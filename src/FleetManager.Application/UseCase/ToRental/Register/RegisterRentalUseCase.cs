using FleetManager.Application.Extensions;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using FleetManager.Domain.DomainExceptionBase;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToClient;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Domain.Repositories.ToRental;
using FleetManager.Domain.Repositories.ToRentalPlans;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRental.Register
{
    public class RegisterRentalUseCase(IRentalReadOnlyRepository rentalReadOnly, ICompanyReadOnlyRepository companyReadOnly, IVehicleReadOnlyRepository vehicleReadOnly,
                                        IClientReadOnlyRepository clientReadOnly, IRentalWriteOnlyRepository repository,
                                        IRentalPlansReadOnlyRepository rentalPlansRead, IUnitOfWork unitOfWork) : IRegisterRentalUseCase
    {
        public async Task<ResponseShortRentalJson> Execute(RequestRentJson request)
        {

            Validate(request);

            var rentalPlan = await ValidateBusinessRules(request);

            var endDate = rentalPlan.Mode == Domain.Enums.RentalMode.Monthly ?
            request.StartDate.AddDays(30) :
            request.EndDate!.Value;

            var rental = new Rental(request.CompanyId, request.ClientId, request.VehicleId, request.StartDate, endDate);

            rental.AttachPlan(rentalPlan);

            if (request.ExtraKm > 0)
                rental.AddExtraKm(request.ExtraKm);

            await repository.Add(rental);
            await unitOfWork.Commit();

            return rental.ToResponse();
        }

        private static void Validate(RequestRentJson request)
        {
            var result = new RentalValidator().Validate(request);
            if (!result.IsValid)
                throw new ErrorOnValidationException([.. result.Errors.Select(x => x.ErrorMessage)]);
        }

        private async Task<RentalPlan> ValidateBusinessRules(RequestRentJson request)
        {
            await EnsureClientExists(request.ClientId);
            var vehicle = await EnsureVehicleExists(request.VehicleId);
            await EnsureVehicleIsAvailable(request.VehicleId);
            await EnsureCompanyExists(request.CompanyId);
            var rentalPlan = await EnsureRentalPlanExists(request.RentalPlanId);


            EnsureVehicleMatchesPlanTransmission(vehicle, rentalPlan);

            return (rentalPlan);
        }

        private async Task<Client> EnsureClientExists(long clientId)
        {
            var result = await clientReadOnly.GetById(clientId);
            return result ?? throw new NotFoundException(ResourceErrorMessages.CLIENT_NOT_FOUND);
        }

        private async Task<Vehicle> EnsureVehicleExists(long id)
        {
            var result = await vehicleReadOnly.GetById(id);
            return result ?? throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
        }

        private async Task<Company> EnsureCompanyExists(long id)
        {
            var result = await companyReadOnly.GetById(id);
            return result ?? throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);
        }

        private async Task<RentalPlan> EnsureRentalPlanExists(long id)
        {
            var result = await rentalPlansRead.GetById(id)
                ?? throw new NotFoundException(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);

            if (!result.IsActive)
                throw new ErrorOnValidationException([ResourceErrorMessages.RENTAL_PLAN_IS_NOT_ACTIVE]);

            return result;
        }

        private static void EnsureVehicleMatchesPlanTransmission(Vehicle vehicle, RentalPlan plan)
        {
            if (vehicle.Category is null)
                throw new DomainRuleException(ResourceErrorMessages.VEHICLE_HAS_NO_CATEGORY);

            if (vehicle.Category.TransmissionType != plan.Transmission)
                throw new DomainRuleException(ResourceErrorMessages.VEHICLE_TRANSMISSION_MISMATCH);
        }
        private async Task EnsureVehicleIsAvailable(long vehicleId)
        {
            var hasActiveRental = await rentalReadOnly.VehicleHasActiveRental(vehicleId);
            if (hasActiveRental)
                throw new DomainRuleException(ResourceErrorMessages.VEHICLE_NOT_AVAILABLE);
        }
    }
}
