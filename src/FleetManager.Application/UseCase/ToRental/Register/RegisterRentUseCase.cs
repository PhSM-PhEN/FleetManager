using System.Net.Http.Headers;
using FleetManager.communication.Requests;
using FleetManager.communication.Responses;
using FleetManager.Domain.DomainExceptionBase;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToClient;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Domain.Repositories.ToRentalPlans;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Domain.Services.LoggeUser;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRental.Register
{
    public class RegisterRentUseCase(
        ILoggedUser loggedUser, ICompanyReadOnlyRepository companyReadOnly,
        IVehicleReadOnlyRepository vehicleReadOnly, IClientReadOnlyRepository clientReadOnly,
        IRentalPlansReadOnlyRepository rentalPlansRead, IUnitOfWork unitOfWork) : IRegisterRentUseCase
    {

        public async Task<ResponseRentalJson> Execute(RequestRentJson request)
        { 
            var logged = await loggedUser.Get();
            Validate(request);
            await ValidateBusinessRules(request);
            
            
            await unitOfWork.Commit();
            throw new NotImplementedException();
        }
        private static void Validate(RequestRentJson request)
        {
            var validate = new RentalValidator();
            var result = validate.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
        private async Task<(Vehicle vehicle, RentalPlan rentalPlan)> ValidateBusinessRules(RequestRentJson request)
        {
            await EnsureClientExists(request.ClientId);
            var vehicle = await EnsureVehicleExists(request.VehicleId);
            await EnsureCompanyExists(request.CompanyId);
            var rentalPlan = await EnsureRentalPlanExists(request.RentalPlanId);
            EnsureVehicleMatchesPlanTransmission(vehicle, rentalPlan);
            return (vehicle, rentalPlan);
           
        }
        private static void EnsureVehicleMatchesPlanTransmission(Vehicle vehicle, RentalPlan plan)
        {
            if (vehicle.Transmission != plan.Transmission)
            throw new DomainRuleException("VEHICLE_TRANSMISSION_MISMATCH");
        } 
        private async Task EnsureClientExists(long clientId)

        {
            var result = await clientReadOnly.GetById(clientId);
            
            if(result == null)
            {
                throw new NotFoundException(ResourceErrorMessages.CLIENT_NOT_FOUND);
            }
        }
       
        private async Task<Vehicle> EnsureVehicleExists(long id)
        {
            var result = await vehicleReadOnly.GetById(id);
            if(result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
            }
            return result;
        }
       
        private async Task EnsureCompanyExists(int id)
        {
            var result = await companyReadOnly.GetById(id);
            if(result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);
            }
        }
        private async Task<RentalPlan> EnsureRentalPlanExists(int id)
        {
            var result = await rentalPlansRead.GetById(id);
            if(result is null)
            {
                throw new NotFoundException("rental plan not found");
            }
            if(result.IsActive is false)
            {
                throw new ErrorOnValidationException(["Rental plan not this active"]);
            }
            return result;
        }
        
       
    }
    

}
