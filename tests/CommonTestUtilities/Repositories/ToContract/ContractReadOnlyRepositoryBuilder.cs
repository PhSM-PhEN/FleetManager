using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToContract;
using Moq;

namespace CommonTestUtilities.Repositories.ToContract
{
    public class ContractReadOnlyRepositoryBuilder
    {
        private readonly Mock<IContractReadOnlyRepository> _repository;

        public ContractReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IContractReadOnlyRepository>();
        }

        public ContractReadOnlyRepositoryBuilder GetById(long id, Contract? contract)
        {
            _repository.Setup(c => c.GetById(id)).ReturnsAsync(contract);
            return this;
        }

        public ContractReadOnlyRepositoryBuilder GetAll(List<Contract> contracts, int pageNumber, int pageSize, int totalCount)
        {
            _repository.Setup(c => c.GetAll(pageNumber, pageSize)).ReturnsAsync((contracts, totalCount));
            return this;
        }

        public IContractReadOnlyRepository Build() => _repository.Object;
    }
}