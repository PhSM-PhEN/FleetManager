using FleetManager.communication.Requests;
using FleetManager.communication.Responses;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Domain.Repositories.ToClient;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Domain.Services.LoggeUser;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRental.Register
{
    public class RegisterRentUseCase(
        ILoggedUser loggedUser, ICompanyReadOnlyRepository companyReadOnly,
        IVehicleReadOnlyRepository vehicleReadOnly, ICategoryReadOnlyRepository categoryReadOnly,
        IClientReadOnlyRepository clientReadOnly, IUnitOfWork unitOfWork) : IRegisterRentUseCase
    {
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly ICompanyReadOnlyRepository _companyReadOnly = companyReadOnly;
        private readonly IVehicleReadOnlyRepository _vehicleReadOnly = vehicleReadOnly;
        private readonly ICategoryReadOnlyRepository _categoryReadOnly = categoryReadOnly;
        private readonly IClientReadOnlyRepository _clientReadOnly = clientReadOnly;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ResponseRentalJson> Execute(RequestRentJson request)
        { 
            var loggedUser = await _loggedUser.Get();
            Validate(request);
            await ValidateBusinessRules(request);
            
            
            await _unitOfWork.Commit();
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
        private async Task ValidateBusinessRules(RequestRentJson request)
        {
            await EnsureCategoryExists(request.CompanyId);
            await EnsureClientExists(request.ClientId);
            await EnsureVehicleExists(request.VehicleId);
            await EnsureCompanyExists(request.CompanyId);
            await EnsureCategoryExists(request.CategoryId);
        }
        private async Task EnsureClientExists(long clientId)

        {
            var result = await _clientReadOnly.GetById(clientId);
            
            if(result == null)
            {
                throw new NotFoundException(ResourceErrorMessages.CLIENT_NOT_FOUND);
            }
        }
       
        private async Task EnsureVehicleExists(long id)
        {
            var result = await _vehicleReadOnly.GetById(id);
            if(result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
            }
        }
        private async Task EnsureCategoryExists(int id)
        {
            var result = await _categoryReadOnly.GetById(id);
            if(result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);
            }
        }
        private async Task EnsureCompanyExists(int id)
        {
            var result = await _companyReadOnly.GetById(id);
            if(result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);
            }
        }
       
       
    }
    

}
