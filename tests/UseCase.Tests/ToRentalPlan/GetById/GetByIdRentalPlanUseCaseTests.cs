using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories.ToRentalPlan;
using FleetManager.Application.UseCase.ToRentalPlan.GetById;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToRentalPlan.GetById
{
    public class GetByIdRentalPlanUseCaseTests
    {
        [Fact]
        public async Task Success()
        {
            var rentalPlan = RentalPlanBuilder.Build(1);

            var useCase = CreateUseCase(rentalPlan);
            var result = await useCase.Execute(rentalPlan.Id);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(rentalPlan.Name);
            result.DailyPrice.ShouldBe(rentalPlan.DailyPrice);
            result.MileagePerMonthly.ShouldBe(rentalPlan.MileagePerMonthly);
        }

        [Fact]
        public async Task Error_Pricing_Not_Found()
        {
            var rentalPlan = RentalPlanBuilder.Build(1);
            var useCase = CreateUseCase(rentalPlan);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);
        }

        private static GetByIdRentalPlanUseCase CreateUseCase(RentalPlan vehiclePricing)
        {
            var repository = new RentalPlanReadOnlyRepositoryBuilder()
                .GetById(vehiclePricing)
                .Build();

            return new GetByIdRentalPlanUseCase(repository);
        }
    }
}
