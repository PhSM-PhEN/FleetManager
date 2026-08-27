using FleetManager.Domain.Entities;
using FleetManager.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.Migrations
{
    public static class ContractTemplateSeeder
    {
        private const long SystemUserId = 0;

        public static async Task SeedDefaultTemplates(FleetManagerDbContext dbContext)
        {
            var alreadyHasTemplates = await dbContext.ContractTemplates.AnyAsync();
            if (alreadyHasTemplates)
                return;

            var defaultTemplate = new ContractTemplate(
                name: "Modelo Padrão - Locação de Veículo",
                content: DefaultContractTemplateContent.Standard,
                version: 1);

            defaultTemplate.SetCreatedBy(SystemUserId);
            defaultTemplate.Activate();

            await dbContext.ContractTemplates.AddAsync(defaultTemplate);
            await dbContext.SaveChangesAsync();
        }
    }
}
