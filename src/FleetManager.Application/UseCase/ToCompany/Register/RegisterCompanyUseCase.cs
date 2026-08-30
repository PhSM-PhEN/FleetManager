using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToCompany;
using FleetManager.Communication.Response.ToCompany;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Entities.ValueObjects;
using FleetManager.Domain.Enum;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.Register
{
    public class RegisterCompanyUseCase(
        ICompanyWriteOnlyRepository repository,
        ICompanyReadOnlyRepository readOnlyRepository,
        IAddressReadOnlyRepository addressReadOnly,
        IUnitOfWork unitOfWork) : IRegisterCompanyUseCase
    {
        public async Task<ResponseShortCompanyJson> Execute(RequestCompanyJson request)
        {
            await Validate(request);


            var contact = new Contact(request.PhoneNumber, request.Email);


            TaxRegimeEnum? taxRegime = null;

            if (!string.IsNullOrWhiteSpace(request.TaxRegime))
            {
                taxRegime = Enum.Parse<TaxRegimeEnum>(request.TaxRegime);
            }



            var company = new Company(request.LegalName, request.TradeName, request.StateRegistration, request.MunicipalRegistration, request.PrimaryCnae, taxRegime, request.Cnpj, contact, request.AddressId);

            await repository.Add(company);
            await unitOfWork.Commit();

            return company.ToShortResponse();
        }

        private async Task Validate(RequestCompanyJson request)
        {
            var validator = new CompanyValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }

            _ = await addressReadOnly.GetById(request.AddressId) ??
                throw new NotFoundException(ResourceErrorMessages.ADDRESS_NOT_FOUND);

            if (await readOnlyRepository.ExistByCnpj(request.Cnpj))
            {
                throw new ErrorOnValidationException([ResourceErrorMessages.CNPJ_ALREADY_REGISTERED]);
            }
        }
    }
}
