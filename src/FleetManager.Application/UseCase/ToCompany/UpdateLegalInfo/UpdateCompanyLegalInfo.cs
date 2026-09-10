using FleetManager.Communication.Request.ToCompany;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.UpdateLegalInfo
{
    public class UpdateCompanyLegalInfo(ICompanyWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IUpdateCompanyLegalInfo
    {
        public async Task Execute(long id, RequestCompanyUpdateLegalInfoJson request)
        {
            var company = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);

            company.UpdateLegalInfo(request.LegalName, request.StateRegistration, request.MunicipalRegistration, request.PrimaryCnae);

            repository.Update(company);
            await unitOfWork.Commit();

           
        }
    }
}
