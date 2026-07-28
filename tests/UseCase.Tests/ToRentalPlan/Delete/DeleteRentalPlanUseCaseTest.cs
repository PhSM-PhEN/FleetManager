using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToRentalPlan;
using FleetManager.Application.UseCase.ToRentalPlan.Delete;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToRentalPlan.Delete
{
    public class DeleteRentalPlanUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var rentalPlan = RentalPlanBuilder.Build(1);
            var useCase = CreateUseCase(rentalPlan);

            await useCase.Execute(rentalPlan.Id);
        }

        [Fact]
        public async Task Error_RentalPlan_Not_Found()
        {
            var useCase = CreateUseCase(rentalPlan: null);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);
        }

        private static DeleteRentalPlanUseCase CreateUseCase(RentalPlan? rentalPlan)
        {
            var repositoryBuilder = new RentalPlanWriteOnlyRepositoryBuilder()
                .Delete(rentalPlan?.Id ?? 999);

            if (rentalPlan is not null)
                repositoryBuilder.GetById(rentalPlan);

            var repository = repositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteRentalPlanUseCase(repository, unitOfWork);
        }
    }
}
