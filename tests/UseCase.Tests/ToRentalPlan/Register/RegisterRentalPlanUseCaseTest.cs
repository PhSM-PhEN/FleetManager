using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToRentalPlan;
using CommonTestUtilities.Request.ToRentalPlan;
using FleetManager.Application.UseCase.ToRentalPlan.Register;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToRentalPlan.Register
{
    public class RegisterRentalPlanUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestRentalPlanJsonBuilder.Build();

            var useCase = CreateUseCase(rentalPlan);
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(request.Name);
            result.DailyPrice.ShouldBe(request.DailyPrice);
        }

        [Fact]
        public async Task Error_DailyPrice_Zero()
        {
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestRentalPlanJsonBuilder.Build();
            request.DailyPrice = 0;

            var useCase = CreateUseCase(rentalPlan);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.DAILY_PRICE_INVALID);
        }


        private static RegisterRentalPlanUseCase CreateUseCase(RentalPlan rentalPlan)
        {
            var writeRepository = new RentalPlanWriteOnlyRepositoryBuilder()
                .Add(rentalPlan)
                .Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RegisterRentalPlanUseCase(writeRepository, unitOfWork);
        }
    }
}
