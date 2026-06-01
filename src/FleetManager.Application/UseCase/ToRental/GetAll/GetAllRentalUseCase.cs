using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToRental;


namespace FleetManager.Application.UseCase.ToRental.GetAll;

public class GetAllRentalUseCase(IRentalReadOnlyRepository repository) : IGetAllRentalUseCase
{
    public async Task<ResponseListRentalJson> Execute()
    {
        var rental = await repository.GetAll();
      
        return new ResponseListRentalJson
        {
            Rentals = [.. rental.Select(r => new ResponseRentalJson
            {
               Id = r.Id,
               CompanyName = r.Company.Name,
               ClientName = r.Client.FirstAndLastName,
               VehicleModel = r.Vehicle.Model,
               TotalPrice = r.TotalPrice
            })]


        };
    }   
    
   
}
