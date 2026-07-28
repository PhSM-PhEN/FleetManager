namespace FleetManager.Application.UseCase.ToRentalPlan.Delete
{
    public interface IDeleteRentalPlanUseCase
    {
        Task Execute(long id);
    }
}
