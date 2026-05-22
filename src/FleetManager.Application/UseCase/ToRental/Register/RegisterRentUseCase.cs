using FleetManager.communication.Requests.ToRental;
using FleetManager.communication.Responses.ToRental;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Domain.Repositories.ToClient;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Domain.Services.LoggeUser;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRental.Register
{
    public class RegisterRentUseCase(ILoggedUser loggedUser, ICompanyReadOnlyRepository companyReadOnly,
                                    IVehicleReadOnlyRepository vehicleReadOnly, ICategoryReadOnlyRepository categoryReadOnly,
                                    IClientReadOnlyRepository clientReadOnly, IAddressReadOnlyRepository addressReadOnly, IUnitOfWork unitOfWork) : IRegisterRentUseCase
    {
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly ICompanyReadOnlyRepository _companyReadOnly = companyReadOnly;
        private readonly IVehicleReadOnlyRepository _vehicleReadOnly = vehicleReadOnly;
        private readonly ICategoryReadOnlyRepository _categoryReadOnly = categoryReadOnly;
        private readonly IClientReadOnlyRepository _clientReadOnly = clientReadOnly;
        private readonly IAddressReadOnlyRepository _addressReadOnly = addressReadOnly;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public Task<ResponseRentalJson> Execute(RequestRentJson request)
        { 
            Validate(request);
            
            
            throw new NotImplementedException();
        }
        private void Validate(RequestRentJson request)
        {
            var validate = new RentalValidator();
            var result = validate.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
        private async Task EnsureClientExist(long clientId)
        {
            var result = await _clientReadOnly.GetById(clientId);

        }
    }
    

}
