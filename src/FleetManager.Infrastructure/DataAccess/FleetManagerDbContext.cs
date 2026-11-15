using FleetManager.Domain.Entities;
using FleetManager.Domain.Services.LoggedUser;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FleetManager.Infrastructure.DataAccess
{
    public class FleetManagerDbContext(DbContextOptions dbContextOptions, ILoggedUser loggedUser) : DbContext(dbContextOptions)
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<RentalPlan> RentalPlans { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<ContractTemplate> ContractTemplates { get; set; }
        public DbSet<ContractDocument> ContractDocuments { get; set; }

        public DbSet<HistoryLog> HistoryLogs { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            // 1. Filtra apenas as entidades que herdam de AuditableEntity
            var auditableEntries = ChangeTracker.Entries<AuditableEntity>()
                .Where(e => e.State == EntityState.Added
                         || e.State == EntityState.Modified
                         || e.State == EntityState.Deleted)
                .ToList();

            // 2. Se for um cadastro de User (ou algo não auditável), salva normal e vai embora!
            if (auditableEntries.Count == 0)
                return await base.SaveChangesAsync(ct);

            // 3. Se chegou aqui, ESTÁ alterando uma AuditableEntity. 
            // Obrigatoriamente precisamos de um usuário logado. 
            // Se não tiver token, o método Get() lança sua UnauthorizedException e aborta tudo.
            var user = await loggedUser.GetLoggedUserAsync();

            var pendingHistory = new List<(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> Entry, string Action)>();

            // 4. Preenche os dados de auditoria em memória
            foreach (var entry in auditableEntries)
            {
                string action = string.Empty;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.SetCreatedBy(user.Id);
                        action = "Created";
                        break;

                    case EntityState.Modified:
                        // Se foi um Update comum, o LastAction é nulo e vira "Updated".
                        // Se o UseCase chamou entity.SoftDelete(), o LastAction é "Deleted".
                        entry.Entity.SetUpdatedBy(user.Id);
                        action = entry.Entity.LastAction ?? "Updated";
                        break;

                    case EntityState.Deleted:
                        // TRAVA DE SEGURANÇA:
                        // Como a arquitetura define que o UseCase faz o Soft Delete, 
                        // se o estado chegar aqui como "Deleted", alguém usou _dbContext.Remove() indevidamente.
                        throw new InvalidOperationException(
                            $"Hard Delete não é permitido para a entidade {entry.Metadata.ClrType.Name}. " +
                            "Utilize o método entity.SoftDelete() através do UseCase."
                        );
                }

                pendingHistory.Add((entry, action));
                entry.Entity.ClearHistoryEvent();
            }

            // 1. Checa se o banco suporta transações (MySQL suporta, InMemory não)
            var isRelational = Database.IsRelational();

            // 2. Só abre a transação se for relacional
            var transaction = isRelational ? await Database.BeginTransactionAsync(ct) : null;

            try
            {
                // Salva os dados principais
                var result = await base.SaveChangesAsync(ct);

                if (pendingHistory.Count > 0)
                {
                    foreach (var (entry, action) in pendingHistory)
                    {
                        HistoryLogs.Add(new HistoryLog(
                            entityName: entry.Metadata.ClrType.Name,
                            entityId: entry.Entity.Id,
                            action: action,
                            performedBy: user.Id,
                            performedByName: user.Name
                        ));
                    }

                    // Salva os logs
                    await base.SaveChangesAsync(ct);
                }

                // 3. Só commita se a transação foi criada
                if (transaction is not null)
                    await transaction.CommitAsync(ct);

                return result;
            }
            catch
            {
                // 4. Só faz rollback se a transação existir
                if (transaction is not null)
                    await transaction.RollbackAsync(ct);

                throw;
            }
            finally
            {
                // 5. Garante o descarte da transação da memória
                if (transaction is not null)
                    await transaction.DisposeAsync();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();


            modelBuilder.Entity<User>()
                .Property<int?>("AdminSlot")
                .HasComputedColumnSql("CASE WHEN `Role` = 'Admin' THEN 1 ELSE NULL END", stored: true);

            modelBuilder.Entity<User>()
                .HasIndex("AdminSlot")
                .IsUnique()
                .HasDatabaseName("UX_Users_SingleAdmin");

            modelBuilder.Entity<Tenant>()
                .OwnsOne(t => t.DriverLicense);

            modelBuilder.Entity<Tenant>()
                .OwnsOne(t => t.Contact);

            modelBuilder.Entity<Tenant>()
                .OwnsOne(t => t.Cpf, cpf =>
                {
                    cpf.HasIndex(c => c.Number).IsUnique();
                });

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Cnpj)
                .IsUnique();

            modelBuilder.Entity<Company>()
                .OwnsOne(c => c.Contact);

            modelBuilder.Entity<Vehicle>()
                .OwnsOne(v => v.ManufacturingYear);

            modelBuilder.Entity<Vehicle>()
                .OwnsOne(v => v.Renavam, renavam =>
                {
                    renavam.HasIndex(r => r.Number).IsUnique();
                });

            modelBuilder.Entity<Vehicle>()
                .OwnsOne(v => v.ChassiNumber, chassi =>
                {
                    chassi.HasIndex(c => c.Number).IsUnique();
                });

            modelBuilder.Entity<Vehicle>()
                .OwnsOne(v => v.LicensePlate, plate =>
                {
                    plate.HasIndex(p => p.Number).IsUnique();
                });

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Company)
                .WithMany()
                .HasForeignKey(v => v.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.RentalPlan)
                .WithMany(p => p.Vehicles)
                .HasForeignKey(v => v.RentalPlanId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Vehicle)
                .WithMany()
                .HasForeignKey(c => c.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Tenant)
                .WithMany()
                .HasForeignKey(c => c.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.RentalPlan)
                .WithMany()
                .HasForeignKey(c => c.RentalPlanId)
                .OnDelete(DeleteBehavior.Restrict);

            // Concorrência no Register: dois requests simultâneos pro mesmo veículo não podem
            // resultar em dois contratos "ativos" (Reserved/Active/Overdue). A checagem em memória
            // (HasActiveContract) sozinha não impede a corrida entre o check e o commit — quem
            // garante isso de verdade é essa constraint única no banco, avaliada atomicamente pelo
            // MySQL. Contratos Finished/Cancelled/Renewed geram NULL, que não conflita entre si.
            // 1 = Reserved, 2 = Active, 5 = Overdue (ContractStatus enum) — mesmos status usados
            // em ContractRepository.HasActiveContract.
            modelBuilder.Entity<Contract>()
                .Property<long?>("ActiveVehicleId")
                .HasComputedColumnSql("CASE WHEN `Status` IN (1, 2, 5) THEN `VehicleId` ELSE NULL END", stored: true);

            modelBuilder.Entity<Contract>()
                .HasIndex("ActiveVehicleId")
                .IsUnique()
                .HasDatabaseName("UX_Contracts_ActiveVehicle");
            modelBuilder.Entity<Maintenance>()
                .HasOne(m => m.Vehicle)
                .WithMany()
                .HasForeignKey(m => m.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

   
            modelBuilder.Entity<Charge>()
                .HasOne(c => c.Contract)
                .WithMany()
                .HasForeignKey(c => c.ContractId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ContractDocument>()
                .HasOne(d => d.Contract)
                .WithMany()
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            // Garante no máximo um ContractTemplate ativo por vez a nível de banco também
            // (a regra em código já impede isso no ActivateContractTemplateUseCase).
            // modelBuilder.Entity<ContractTemplate>()
            //     .Property<bool?>("ActiveFlag")
            //     .HasComputedColumnSql("CASE WHEN `IsActive` = 1 THEN 1 ELSE NULL END", stored: true);

            // modelBuilder.Entity<ContractTemplate>()
            //     .HasIndex("ActiveFlag")
            //     .IsUnique()
            //     .HasDatabaseName("UX_ContractTemplates_SingleActive");


            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(t => t.BaseType is null && typeof(AuditableEntity).IsAssignableFrom(t.ClrType)))
            {
                var param = Expression.Parameter(entityType.ClrType, "e");
                var isDeleted = Expression.Property(param, nameof(AuditableEntity.IsDeleted));
                entityType.SetQueryFilter(Expression.Lambda(Expression.Not(isDeleted), param));
            }

            


        }



    }
}
