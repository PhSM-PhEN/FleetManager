namespace FleetManager.Application.UseCase.ToRental.Delete;

public interface IDeleteRentalUseCase
{
    Task Execute(long id);
}
