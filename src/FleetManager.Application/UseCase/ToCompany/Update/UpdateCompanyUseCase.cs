using FleetManager.Communication.Request.ToCompany;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.Update
{
    public class UpdateCompanyUseCase(
        ICompanyWriteOnlyRepository repository,
        IAddressReadOnlyRepository addressReadOnly,
        IUnitOfWork unitOfWork) : IUpdateCompanyUseCase
    {
        public async Task Execute(long id, RequestCompanyJson request)
        {
            var company = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);

            Validate(request);

            _ = await addressReadOnly.GetById(request.AddressId) ??
                throw new NotFoundException(ResourceErrorMessages.ADDRESS_NOT_FOUND);

            
            company.Update(request.Name, request.PhoneNumber, request.AddressId);

            repository.Update(company);
            await unitOfWork.Commit();
        }

        private static void Validate(RequestCompanyJson request)
        {
            var validator = new CompanyValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}