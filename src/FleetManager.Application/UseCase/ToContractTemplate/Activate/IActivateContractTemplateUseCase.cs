namespace FleetManager.Application.UseCase.ToContractTemplate.Activate
{
    public interface IActivateContractTemplateUseCase
    {
        Task Execute(long id);
    }
}