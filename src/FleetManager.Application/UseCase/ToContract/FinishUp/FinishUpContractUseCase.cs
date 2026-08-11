using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCharge;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Exception.ExceptionBase;
using FleetManager.Domain.Repositories.ToVehicle;

namespace FleetManager.Application.UseCase.ToContract.FinishUp
{
    public class FinishUpContractUseCase(
        IContractWriteOnlyRepository contractRepository,
        IChargeWriteOnlyRepository chargeRepository,
        IVehicleWriteOnlyRepository vehicleWrite,
        IUnitOfWork unitOfWork) : IFinishUpContractUseCase
    {
        public async Task<ResponseFinishUpContractJson> Execute(long id, RequestFinishUpContractJson request)
        {
            Validate(request);

            var contract = await contractRepository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);
            var vehicle = await vehicleWrite.GetById(contract.VehicleId) ??
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);

            var actualReturnDateTime = DateTime.UtcNow;
            


            contract.FinishUp(actualReturnDateTime, request.FinalMileage);
            vehicle.UpdateMileage(request.FinalMileage);

            vehicleWrite.Update(vehicle);
            contractRepository.Update(contract);
            if(contract.ExcessMileageFee != null)
            {
                var excessMileage = Charge.ForLateFee(contract);
                await chargeRepository.Add(excessMileage);
                
            }

            if (contract.LateFee is > 0)
            {
                var lateFeeCharge = Charge.ForLateFee(contract);
                await chargeRepository.Add(lateFeeCharge);
            }


            await unitOfWork.Commit();
            return contract.ToFinishUpResponse();
        }

        private static void Validate(RequestFinishUpContractJson request)
        {
            var validator = new FinishUpContractValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
