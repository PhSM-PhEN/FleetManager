using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Request.ToVehicle;
using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCharge;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Application.UseCase.ToVehicle.Update;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Complete
{
    public class CompleteContractUseCase(
        IContractWriteOnlyRepository contractRepository,
        IChargeWriteOnlyRepository chargeRepository,
        IUpdateMileageVehicleUseCase updateMileageVehicleUseCase,
        IUnitOfWork unitOfWork) : ICompleteContractUseCase
    {
        public async Task<ResponseCompleteContractJson> Execute(long id, RequestCompleteContractJson request)
        {
            Validate(request);

            var contract = await contractRepository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);

            var actualReturnDateTime = request.ActualReturnDateTime ?? DateTime.UtcNow;

            contract.Complete(actualReturnDateTime, request.FinalMileage);
            contractRepository.Update(contract);

            if (contract.LateFee is > 0)
            {
                var lateFeeCharge = Charge.ForLateFee(contract);
                await chargeRepository.Add(lateFeeCharge);
            }

            await unitOfWork.Commit();

            // Contrato encerrado: o fluxo passa a bola para o módulo de veículo cuidar da própria
            // quilometragem — Complete não conhece/mexe em regra de Vehicle, só aciona o próximo passo.
            // Atenção: isso roda em uma transação separada do commit acima (o caso de uso de
            // quilometragem faz seu próprio unitOfWork.Commit()). Se essa segunda etapa falhar
            // (ex.: quilometragem menor que a atual do veículo), o contrato já estará Finished e a
            // multa já estará cobrada — não há rollback automático do primeiro commit.
            await updateMileageVehicleUseCase.Execute(contract.VehicleId, new RequestMileageVehicleJson
            {
                MileageVehicle = request.FinalMileage
            });

            return contract.ToCompleteResponse();
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
