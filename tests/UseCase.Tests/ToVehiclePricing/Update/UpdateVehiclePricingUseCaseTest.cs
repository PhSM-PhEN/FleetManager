using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToVehiclePricing;
using FleetManager.Application.UseCase.ToVehiclePricing.Update;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToVehiclePricing.Update
{
    public class UpdateVehiclePricingUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestRentalPlanJsonBuilder.Build();

            var useCase = CreateUseCase(rentalPlan);
            await useCase.Execute(rentalPlan.Id, request);

            rentalPlan.DailyPrice.ShouldBe(request.DailyPrice);
            rentalPlan.MonthlyPrice.ShouldBe(request.MonthlyPrice);
            rentalPlan.ExcessMileageRate.ShouldBe(request.ExcessMileageRate);
            rentalPlan.MileagePerDay.ShouldBe(request.MileagePerDay);
            rentalPlan.MileagePerMonthly.ShouldBe(request.MileagePerMonthly);
        }

        [Fact]
        public async Task Error_Pricing_Not_Found()
        {
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestRentalPlanJsonBuilder.Build();

            var useCase = CreateUseCase(rentalPlan);
            var act = async () => await useCase.Execute(999, request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);
        }

        [Fact]
        public async Task Error_MonthlyPrice_Zero()
        {
            var rentalPlan = RentalPlanBuilder.Build();
            var request = RequestRentalPlanJsonBuilder.Build();
            request.MonthlyPrice = 0;

            var useCase = CreateUseCase(rentalPlan);
            var act = async () => await useCase.Execute(rentalPlan.Id, request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.MONTHLY_PRICE_INVALID);
        }

        private static UpdateVehiclePricingUseCase CreateUseCase(RentalPlan rentalPlan)
        {
            var repository = new VehiclePricingWriteOnlyRepositoryBuilder()
                .GetById(rentalPlan)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new UpdateVehiclePricingUseCase(repository, unitOfWork);
        }
    }
}
