using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToAddress;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class AddressWriteOnlyRepositoryBuilder
    {
        private readonly Mock<IAddressWriteOnlyRepository> _repository;

        public AddressWriteOnlyRepositoryBuilder()
        {
            _repository = new Mock<IAddressWriteOnlyRepository>();
        }

        public AddressWriteOnlyRepositoryBuilder Add(Address address)
        {
            _repository.Setup(a => a.Add(address)).Returns(Task.CompletedTask);
            return this;
        }

        public AddressWriteOnlyRepositoryBuilder Delete(long id)
        {
            _repository.Setup(a => a.Delete(id)).Returns(Task.CompletedTask);
            return this;
        }

        public AddressWriteOnlyRepositoryBuilder GetById(long id, Address address)
        {
            _repository.Setup(a => a.GetById(id)).ReturnsAsync(address);
            return this;
        }

        public AddressWriteOnlyRepositoryBuilder Update(Address address)
        {
            _repository.Setup(a => a.Update(address));
            return this;
        }

        public IAddressWriteOnlyRepository Build() => _repository.Object;
    }
}