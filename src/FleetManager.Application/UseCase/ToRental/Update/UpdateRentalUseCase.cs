using AutoMapper;
using FleetManager.Communication.Requests;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToRental;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRental.Update;

public class UpdateRentalUseCase(IRentalUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IUpdateRentalUseCase
{
    public async Task Execute(long id, RequestRentJson request)
    {
        Validate(request);
        var rental = await repository.GetById(id);
        if(rental is null)
        {
            throw new NotFoundException("Rental not found");
        }
        mapper.Map(request, rental);
        repository.Update(rental);
        await unitOfWork.Commit();
        
    }
    private static void Validate(RequestRentJson request)
    {
        var Validator = new RentalValidator();
        var result = Validator.Validate(request);

        if(result.IsValid == false)
        {
            var error = result.Errors.Select(erro => erro.ErrorMessage).ToList();
            throw new ErrorOnValidationException(error);
        }
    }
}
