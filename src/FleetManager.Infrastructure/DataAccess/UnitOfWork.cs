using FleetManager.Domain.Repositories;
using FleetManager.Exception.ExceptionBase;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace FleetManager.Infrastructure.DataAccess
{
    internal class UnitOfWork(FleetManagerDbContext dbContext) : IUnitOfWork
    {
        // Constraints únicas que existem só pra impedir corrida de concorrência (não são erros
        // de validação do usuário) e o erro de negócio correspondente pra cada uma. Centralizado
        // aqui porque é o único ponto em que uma violação de índice do MySQL realmente aparece
        // (SaveChangesAsync), então não faz sentido a camada de Application conhecer MySqlConnector.
        private static readonly Dictionary<string, string> ConcurrencyConstraintMessages = new()
        {
            ["UX_Contracts_ActiveVehicle"] = ResourceErrorMessages.VEHICLE_ALREADY_RENTED
        };

        public async Task Commit()
        {
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is MySqlException { Number: 1062 } mySqlException)
            {
                var constraint = ConcurrencyConstraintMessages.Keys
                    .FirstOrDefault(key => mySqlException.Message.Contains(key));

                if (constraint is null)
                    throw;

                throw new BusinessRuleException(ConcurrencyConstraintMessages[constraint]);
            }
        }
    }
}
