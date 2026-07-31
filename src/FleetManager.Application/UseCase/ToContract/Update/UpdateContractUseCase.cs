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
            
            var contract = await repository.GetById(id) ?? 
                throw new NotFoundException("");
            var totalDays = CalculatePeriod(contract.RentalType ,request.PickupDateTime, request.ReturnDueDateTime);
            contract.Update(contract.RentalPlan, contract.RentalType , request.MileageContracted,
                            request.TotalAmount, totalDays , request.PickupDateTime, request.ReturnDueDateTime);

            repository.Update(contract);
            await unitOfWork.Commit();

        }
        private static (int totalDays, DateTime returnDueDateTime) CalculatePeriod(RentalType rentalType, DateTime pickupDateTime, DateTime? returnDueDateTime)
        {
            if (rentalType == RentalType.Daily)
            {
                var returnDue = returnDueDateTime!.Value;
                return ((returnDue - pickupDateTime).Days, returnDue);
            }

            var monthlyReturnDue = pickupDateTime.AddDays(30);
            return (30, monthlyReturnDue);
        }
        private static void Validate(RequestUpdateContractJson request)
        {
            var validator = new UpdateContractValidator();
            var resuslt = validator.Validate(request);

            if (resuslt.IsValid == false)
            {
                var error = resuslt.Errors.Select(error => error.ErrorMessage).ToList();
                
                throw new ErrorOnValidationException(error);
            }
        }
    }
}
