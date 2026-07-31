using FleetManager.Communication.Request.ToContract;
using FleetManager.Domain.Enum;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Update
{
    public class UpdateContractUseCase(IContractWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IUpdateContractUseCase
    {
        public async Task Execute(long id, RequestUpdateContractJson request)
        {
            Validate(request);

            var contract = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);

            var rentalType = Enum.Parse<RentalType>(request.RentalType);

            contract.Update(contract.RentalPlan, rentalType, request.MileageContracted,
                            request.TotalAmount, request.PickupDateTime, request.ReturnDueDateTime);

            repository.Update(contract);
            await unitOfWork.Commit();
        }

        private static void Validate(RequestUpdateContractJson request)
        {
            var validator =  new UpdateContractValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
