using FleetManager.Communication.Request.ToTenant;
using FleetManager.Domain.Entities.ValueObjects;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToTenant.Update
{
    public class UpdateTenantUseCase(ITenantWriteOnlyRepository repository, IAddressReadOnlyRepository addressReadOnly ,  IUnitOfWork unitOfWork) : IUpdateTenantUseCase
    {
        public async Task Execute(long id, RequestUpdateTenantJson request)
        {
            Valitate(request);
            var tenant = await repository.GetById(id) ?? 
                    throw new NotFoundException(ResourceErrorMessages.TENANT_NOT_FOUND);
            _ = await addressReadOnly.GetById(request.AddressId) ?? 
                    throw new NotFoundException(ResourceErrorMessages.ADDRESS_NOT_FOUND);

            var contact = new Contact(request.PhoneNumber, request.Email);

            tenant.Update(contact, request.AddressId);
            repository.Update(tenant);
            await unitOfWork.Commit();
        }
        private static void Valitate(RequestUpdateTenantJson request)
        {
            var validator = new UpdateTenantValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }

        }
    }
}
