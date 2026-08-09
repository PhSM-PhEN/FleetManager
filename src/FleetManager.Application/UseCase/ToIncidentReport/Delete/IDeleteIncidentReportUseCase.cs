namespace FleetManager.Application.UseCase.ToIncidentReport.Delete
{
    public interface IDeleteIncidentReportUseCase
    {
        Task Execute(long id);
    }
}
