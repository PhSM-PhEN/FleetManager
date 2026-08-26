using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToContract;
using Moq;

namespace CommonTestUtilities.Repositories.ToContract
{
    public class ContractWriteOnlyRepositoryBuilder
    {
        private readonly Mock<IContractWriteOnlyRepository> _repository;

        public ContractWriteOnlyRepositoryBuilder()
        {
            _repository = new Mock<IContractWriteOnlyRepository>();
        }

        public ContractWriteOnlyRepositoryBuilder Add(Contract contract)
        {
            _repository.Setup(c => c.Add(contract)).Returns(Task.CompletedTask);
            return this;
        }

        public ContractWriteOnlyRepositoryBuilder GetById(long id, Contract? contract)
        {
            _repository.Setup(c => c.GetById(id)).ReturnsAsync(contract);
            return this;
        }

        public ContractWriteOnlyRepositoryBuilder Update(Contract contract)
        {
            _repository.Setup(c => c.Update(contract));
            return this;
        }

        public ContractWriteOnlyRepositoryBuilder Delete(Contract contract)
        {
            _repository.Setup(c => c.Delete(contract)).Returns(Task.CompletedTask);
            return this;
        }

        public ContractWriteOnlyRepositoryBuilder GetActiveContractsPastDueDate(List<Contract> contracts)
        {
            _repository.Setup(c => c.GetActiveContractsPastDueDate(It.IsAny<DateTime>())).ReturnsAsync(contracts);
            return this;
        }

        public IContractWriteOnlyRepository Build() => _repository.Object;
    }
}
