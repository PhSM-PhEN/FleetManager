using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Domain.Repositories.ToRentalPlan;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Renew
{
    public class RenewContractUseCase(
        IContractWriteOnlyRepository contractRepository,
        IRentalPlanReadOnlyRepository rentalPlanRepository,
        IUnitOfWork unitOfWork) : IRenewContractUseCase
    {
        public async Task<ResponseShortContractJson> Execute(long id, RequestRenewContractJson request)
        {
            Validate(request);

            var previousContract = await contractRepository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);

            var rentalPlanIdToUse = request.NewRentalPlanId ?? previousContract.RentalPlanId;
            var currentRentalPlan = await rentalPlanRepository.GetById(rentalPlanIdToUse) ??
                throw new NotFoundException(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);

            var renewedContract = Contract.Renew(previousContract, currentRentalPlan, request.MileageContracted);

            contractRepository.Update(previousContract);
            await contractRepository.Add(renewedContract);

            await unitOfWork.Commit();

            return renewedContract.ToResponse();
        }

        private static void Validate(RequestRenewContractJson request)
        {
            var validator = new RenewContractValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}