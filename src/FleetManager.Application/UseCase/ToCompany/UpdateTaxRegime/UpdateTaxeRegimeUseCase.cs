using FleetManager.Communication.Request.ToCompany;
using FleetManager.Domain.Enum;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.UpdateTaxRegime
{
    public class UpdateTaxeRegimeUseCase(ICompanyWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IUpdateTaxeTegimeUseCase
    {
        public async Task Execute(long id, RequestCompanyUpdateTaxRegimeJson request)
        {
            Validate(request);

            var company = await repository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);

            var taxeRegime = Enum.Parse<TaxRegimeEnum>(request.TaxRegime);
            company.UpdateTaxRegime(taxeRegime);

            repository.Update(company);
            await unitOfWork.Commit();
            
            
        }
        private void Validate(RequestCompanyUpdateTaxRegimeJson request)
        {
            var validator = new TaxeRegimeValidator();
            var result = validator.Validate(request);

            if( result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }

}
