using FleetManager.Communication.Request.ToCompany;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.UpdateToAddress
{
    public class UpdateCompanyAddressUseCase(ICompanyWriteOnlyRepository repository, IAddressReadOnlyRepository addressReadOnly, IUnitOfWork unitOfWork) : IUpdateCompanyAddressUseCase
    {
        public async Task Execute(long id, RequestCompanyUpdateAddressJson request)
        {
            var Company = await repository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);
            _= await addressReadOnly.GetById(request.AddressId) ?? throw new NotFoundException(ResourceErrorMessages.ADDRESS_NOT_FOUND);

            Company.UpdateAddress(request.AddressId);
            repository.Update(Company);
             await unitOfWork.Commit();
        }

    }
}
