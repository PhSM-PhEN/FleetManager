namespace FleetManager.Application.UseCase.ToContractTemplate.Deactivate
{
    public interface IDeactivateContractTemplateUseCase
    {
        Task Execute(long id);
    }
}
