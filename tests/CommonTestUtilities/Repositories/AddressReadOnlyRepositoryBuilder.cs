using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToAddress;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class AddressReadOnlyRepositoryBuilder
    {
        private readonly Mock<IAddressReadOnlyRepository> _repository;

        public AddressReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IAddressReadOnlyRepository>();
        }
    
        public AddressReadOnlyRepositoryBuilder GetAll(
            int pageNumber, int pageSize, List<Address> addresses, int totalCount)
        {
            _repository
                .Setup(a => a.GetAll(pageNumber, pageSize))
                .ReturnsAsync((addresses, totalCount));

            return this;
        }

        public AddressReadOnlyRepositoryBuilder GetById(long id, Address? address)
        {
            _repository
                .Setup(a => a.GetById(id))
                .ReturnsAsync(address);

            return this;
        }

        public IAddressReadOnlyRepository Build() => _repository.Object;
    }
}