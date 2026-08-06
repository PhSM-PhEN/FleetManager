using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToIncidentReport;
using FleetManager.Communication.Response.ToIncidentReport;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Domain.Repositories.ToIncidentReport;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToIncidentReport.Register
{
    public class RegisterIncidentReportUseCase(IIncidentReportWriteOnlyRepository repository, IUnitOfWork unitOfWork,
    IVehicleWriteOnlyRepository vehicleWriteOnly, IContractReadOnlyRepository contractReadOnly) : IRegisterIncidentReportUseCase
    {
        public async Task<ResponseIncidentReportJson> Execute(RequestIncidentReportJson request)
        {
            var vehicle = await EnsureVehicleExist(request.VehicleId);
            var contract = await EnsureContractExist(request.ContractId);
            var incidentRisk = Enum.Parse<IncidentRisk>(request.IncidentRisk);

            var incidentReport = new IncidentReport(contract.Id, vehicle.Id, request.Description, incidentRisk);
           
           

            if(incidentReport.IncidentRisk == IncidentRisk.High)
            {
                vehicle.BlockForIncident(incidentReport);
                await vehicleWriteOnly.Add(vehicle);
                
            }
            
            await repository.Add(incidentReport);
            await unitOfWork.Commit();
            return incidentReport.ToResponse();

        }
        private async Task<Vehicle> EnsureVehicleExist(long id)
        {
            return await vehicleWriteOnly.GetById(id) ?? 
                        throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
            
        }
        private async Task<Contract> EnsureContractExist(long id)
        {
            return await contractReadOnly.GetById(id) ??
                         throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);
        }
    }
}
