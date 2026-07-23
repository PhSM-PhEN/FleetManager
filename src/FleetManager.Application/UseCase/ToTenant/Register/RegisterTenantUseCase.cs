using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToTenant;
using FleetManager.Communication.Response.ToRenant;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Entities.ValueObjects;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToTenant.Register
{

    public class RegisterTenantUseCase(ITenantWriteOnlyRepository repository, ITenanteReadOnlyRepository tenantReadOnly ,IAddressReadOnlyRepository addressReadOnly , IUnitOfWork unitOfWork) : IRegisterTenantUseCase

    {
        public async Task<ResponseShortTenantJson> Execute(RequestTenantJson request)
        {
            Validate(request);

           _ = await addressReadOnly.GetById(request.AddressId) ??
                throw new NotFoundException(ResourceErrorMessages.ADDRESS_NOT_FOUND);


            var cpf = new Cpf(request.Cpf);

            if (await tenantReadOnly.ExistByCpf(cpf.Number))
            {
                throw new ErrorOnValidationException([ResourceErrorMessages.CPF_ALREADY_REGISTERED]);
            }

            var driveLicense = new DriverLicense(request.DriverLicenseNumber, request.DriverLicenseCategory);
            var contact = new Contact(request.PhoneNumber, request.Email);
            var tenant = new Tenant(request.Name, cpf , request.Rg, driveLicense, contact, request.AddressId);
            
            await repository.Add(tenant);
            await unitOfWork.Commit();
            return tenant.ToResponse();
        }
        private static void Validate(RequestTenantJson request)
        {
            var validator = new TenantValidator();
            var result = validator.Validate(request);

            if(result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}