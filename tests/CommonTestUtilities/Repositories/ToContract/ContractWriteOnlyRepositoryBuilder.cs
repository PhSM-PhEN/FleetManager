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

        public ContractWriteOnlyRepositoryBuilder Delete(long id)
        {
            _repository.Setup(c => c.Delete(id)).Returns(Task.CompletedTask);
            return this;
        }

        public ContractWriteOnlyRepositoryBuilder HasActiveContract(long vehicleId, bool hasActive)
        {
            _repository.Setup(c => c.HasActiveContract(vehicleId)).ReturnsAsync(hasActive);
            return this;
        }

        public IContractWriteOnlyRepository Build() => _repository.Object;
    }
}