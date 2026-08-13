using FleetManager.Domain.Repositories;
using FleetManager.Exception.ExceptionBase;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace FleetManager.Infrastructure.DataAccess
{
    internal class UnitOfWork(FleetManagerDbContext dbContext) : IUnitOfWork
    {
        private static readonly Dictionary<string, string> ConcurrencyConstraintMessages = new()
        {
            ["UX_Contracts_ActiveVehicle"] = ResourceErrorMessages.VEHICLE_ALREADY_RENTED
        };

        public async Task Commit()
        {

            var useTransaction = dbContext.Database.IsRelational();

            var transaction = useTransaction
                ? await dbContext.Database.BeginTransactionAsync()
                : null;

            try
            {
                await dbContext.SaveChangesAsync();

                if (transaction is not null)
                    await transaction.CommitAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is MySqlException { Number: 1062 } mySqlException)
            {
                if (transaction is not null)
                    await transaction.RollbackAsync();

                var constraint = ConcurrencyConstraintMessages.Keys
                    .FirstOrDefault(key => mySqlException.Message.Contains(key));

                if (constraint is null)
                    throw;

                throw new BusinessRuleException(ConcurrencyConstraintMessages[constraint]);
            }
            catch
            {
                if (transaction is not null)
                    await transaction.RollbackAsync();

                throw;
            }
            finally
            {
                if (transaction is not null)
                    await transaction.DisposeAsync();
            }
        }
    }
}