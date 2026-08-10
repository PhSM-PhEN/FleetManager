namespace FleetManager.Application.UseCase.ToContract.DetectOverdue
{
    public interface IDetectOverdueContractsUseCase
    {
        /// <summary>
        /// Varre todos os contratos Active cujo ReturnDueDateTime já passou e os marca como
        /// Overdue. Não calcula nem cobra a multa: isso só acontece quando o contrato é
        /// efetivamente concluído (Complete), com base na data real de devolução.
        /// </summary>
        /// <returns>Quantidade de contratos marcados como atrasados.</returns>
        Task<int> Execute();
    }
}
