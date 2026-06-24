namespace FleetManager.Application.UseCase.ToCategory.Delete
{
    public interface IDeleteCategoryUseCase
    {
        Task Execute(long id);
    }
}
