using FleetManager.Domain.Repositories;
using FleetManager.Exception.ExceptionBase;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace FleetManager.Infrastructure.DataAccess;

internal class UnitOfWork(FleetManagerDbContext dbContext) : IUnitOfWork
{
    private static readonly Dictionary<string, string> ConcurrencyConstraintMessages = new()
    {
        ["UX_Contracts_ActiveVehicle"] = ResourceErrorMessages.VEHICLE_ALREADY_RENTED
    };

    public async Task Commit()
    {
        try
        {
            // O próprio DbContext agora abre a transação, salva os dados, salva o log e commita.
            // Se algo falhar lá dentro, o DbContext faz o Rollback automaticamente.
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is MySqlException { Number: 1062 } mySqlException)
        {
            // Pega o erro de Duplicidade (Unique Constraint) do MySQL e traduz para Regra de Negócio
            var constraint = ConcurrencyConstraintMessages.Keys
                .FirstOrDefault(key => mySqlException.Message.Contains(key));

            if (constraint is null)
                throw; // Lança o erro original se for uma constraint que não mapeamos

            throw new BusinessRuleException(ConcurrencyConstraintMessages[constraint]);
        }
    }
}