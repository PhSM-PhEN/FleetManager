using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToCharge;
using Moq;

namespace CommonTestUtilities.Repositories.ToCharge
{
    public class ChargeWriteOnlyRepositoryBuilder
    {
        private readonly Mock<IChargeWriteOnlyRepository> _repository;

        public ChargeWriteOnlyRepositoryBuilder()
        {
            _repository = new Mock<IChargeWriteOnlyRepository>();
        }

        public ChargeWriteOnlyRepositoryBuilder Add()
        {
            _repository.Setup(c => c.Add(It.IsAny<Charge>())).Returns(Task.CompletedTask);
            return this;
        }

        public Mock<IChargeWriteOnlyRepository> BuildMock() => _repository;

        public IChargeWriteOnlyRepository Build() => _repository.Object;
    }
}
