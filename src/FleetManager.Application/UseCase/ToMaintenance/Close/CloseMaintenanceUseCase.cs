using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToMaintenance;
using FleetManager.Communication.Response.ToMaintenance;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToMaintenance;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToMaintenance.Close
{
    public class CloseMaintenanceUseCase(IMaintenanceWriteOnlyRepository repository,
        IUnitOfWork unitOfWork) : ICloseMaintenanceUseCase
    {
        public async Task<ResponseMaintenanceJson> Execute(long id, RequestClosedMaintenanceJson request)
        {
            var maintenance = await repository.GetById(id) ?? 
                throw new NotFoundException(ResourceErrorMessages.MAINTENANCE_NOT_FOUND);
            
            maintenance.Close(request.ProblemDescription, request.WorkshopBudget);
            repository.Update(maintenance);
            await unitOfWork.Commit();

            return maintenance.ToInfoResponse();
        }
    }
}
