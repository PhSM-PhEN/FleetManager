namespace FleetManager.Application.UseCase.ToIncidentReport.Resolve
{
    public interface IResolveIncidentReportUseCase
    {
        Task Execute(long id);
    }
}
