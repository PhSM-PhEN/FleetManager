using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToAddress.GetAll;
using FleetManager.Domain.Entities;
using Shouldly;

namespace UseCase.Tests.ToAddress.GetAll
{
    public class GetAllAddressUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var addresses = AddressBuilder.Collection();

            var useCase = CreateUseCase(1, 10, addresses, addresses.Count);
            var result = await useCase.Execute(1, 10);

            result.ShouldNotBeNull();
            result.Data.Count.ShouldBe(addresses.Count);
            result.PageNumber.ShouldBe(1);
            result.PageSize.ShouldBe(10);
            result.TotalCount.ShouldBe(addresses.Count);
        }
        [Fact]
        public async Task Success_Calculates_TotalPages_Correctly()
        {
            var addresses = AddressBuilder.Collection();
            var totalCount = 25;

            var useCase = CreateUseCase(1, 10, addresses, totalCount);
            var result = await useCase.Execute(1, 10);

            result.TotalPages.ShouldBe(3); 
        }

        [Fact]
        public async Task Success_Invalid_PageNumber_Defaults_To_One()
        {
            var addresses = AddressBuilder.Collection();

            var useCase = CreateUseCase(1, 10, addresses, addresses.Count);
            var result = await useCase.Execute(0, 10);

            result.PageNumber.ShouldBe(1);
        }


        [Fact]
        public async Task Success_Invalid_PageSize_Defaults_To_Ten()
        {
            var addresses = AddressBuilder.Collection();

            var useCase = CreateUseCase(1, 10, addresses, addresses.Count);
            var result = await useCase.Execute(1, 100);

            result.PageSize.ShouldBe(10);
        }

        private static GetAllAddressUseCase CreateUseCase(int pageNumber, int pageSize, List<Address> addresses, int totalCount)
        {
            var repository = new AddressReadOnlyRepositoryBuilder()
                .GetAll(pageNumber, pageSize, addresses, totalCount)
                .Build();

            return new GetAllAddressUseCase(repository);
        }
    }
}
