using FleetManager.Communication.Request.ToContract;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCharge;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Complete
{
    public class CompleteContractUseCase(
        IContractWriteOnlyRepository contractRepository,
        IChargeWriteOnlyRepository chargeRepository,
        IVehicleWriteOnlyRepository vehicleRepository,
        IUnitOfWork unitOfWork) : ICompleteContractUseCase
    {
        public async Task Execute(long id, RequestCompleteContractJson request)
        {
            Validate(request);

            var contract = await contractRepository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);

            var vehicle = await vehicleRepository.GetById(contract.VehicleId) ??
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);

            var actualReturnDateTime = request.ActualReturnDateTime ?? DateTime.UtcNow;

            contract.Complete(actualReturnDateTime, request.FinalMileage);
            vehicle.UpdateMileage(request.FinalMileage);

            contractRepository.Update(contract);
            vehicleRepository.Update(vehicle);

            if (contract.LateFee is > 0)
            {
                var lateFeeCharge = Charge.ForLateFee(contract);
                await chargeRepository.Add(lateFeeCharge);
            }

            await unitOfWork.Commit();
        }

        private static void Validate(RequestCompleteContractJson request)
        {
            var validator = new CompleteContractValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
