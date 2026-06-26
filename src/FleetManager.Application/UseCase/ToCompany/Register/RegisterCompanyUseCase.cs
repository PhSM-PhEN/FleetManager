using FleetManager.Application.Extensions;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.Register
{
    public class RegisterCompanyUseCase(ICompanyWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IRegisterCompanyUseCase
    {

        public async Task<ResponseCompanyJson> Execute(RequestCompanyJson request)
        {
            Validate(request);
            var company = new Company(request.Name, request.Cnpj, request.PhoneNumber, request.AddressId);
            await repository.Add(company);
            await unitOfWork.Commit();

            return company.ToResponse();
        }
        private static void Validate(RequestCompanyJson request)
        {
            var Validator = new CompanyValidator();
            var result = Validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessage);
            }
        }
    }
}
