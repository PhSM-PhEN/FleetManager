using FleetManager.Domain.Entities;
using FleetManager.Domain.Services.LoggedUser;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess
{
    public class FleetManagerDbContext(DbContextOptions dbContextOptions, IHttpContextAccessor httpContextAccessor, ILoggedUser loggedUser) : DbContext(dbContextOptions)
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<RentalPlan> RentalPlans { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<IncidentReport> IncidentReports { get; set; }
        public DbSet<Charge> Charges { get; set; }

        public DbSet<HistoryLog> HistoryLogs { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            var pendingHistory = new List<(AuditableEntity entity, string action)>();

            if (httpContextAccessor.HttpContext is not null)
            {

                var auditableEntries = ChangeTracker.Entries<AuditableEntity>()
                        .Where(e => e.State == EntityState.Added
                        || e.State == EntityState.Modified
                        || e.State == EntityState.Deleted)
                        .ToList();
                if (auditableEntries.Count > 0)
                {
                    var user = await loggedUser.Get();

                    foreach (var entry in auditableEntries)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            entry.Entity.SetCreatedBy(user.Id);

                            pendingHistory.Add((entry.Entity, "Created"));
                        }
                        else if (entry.State == EntityState.Modified)
                        {
                            entry.Entity.SetUpdatedBy(user.Id);
                            pendingHistory.Add((entry.Entity, entry.Entity.LastAction ?? "Updated"));
                        }
                        else if (entry.State == EntityState.Deleted)
                        {
                            pendingHistory.Add((entry.Entity, "Deleted"));
                        }

                        entry.Entity.ClearHistoryEvent();
                    }
                }
            }

            var result = await base.SaveChangesAsync(ct);

            if (pendingHistory.Count > 0)
            {
                var user = await loggedUser.Get();
                foreach (var (entity, action) in pendingHistory)
                {
                    HistoryLogs.Add(new HistoryLog(
                        entityName: entity.GetType().Name,
                        entityId: entity.Id,
                        action: action,
                        performedBy: user.Id,
                        performedByName: user.Name));
                }
                await base.SaveChangesAsync(ct);
            }
            return result;

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
                .HasComputedColumnSql("CASE WHEN `ContractStatus` IN (1, 2, 5) THEN `VehicleId` ELSE NULL END", stored: true);

            modelBuilder.Entity<Contract>()
                .HasIndex("ActiveVehicleId")
                .IsUnique()
                .HasDatabaseName("UX_Contracts_ActiveVehicle");
            modelBuilder.Entity<Maintenance>()
                .HasOne(m => m.Vehicle)
                .WithMany()
                .HasForeignKey(m => m.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Maintenance>()
                .HasOne(m => m.IncidentReport)
                .WithMany()
                .HasForeignKey(m => m.IncidentReportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IncidentReport>()
                .HasOne(i => i.Contract)
                .WithMany()
                .HasForeignKey(i => i.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IncidentReport>()
                .HasOne(i => i.Vehicle)
                .WithMany()
                .HasForeignKey(i => i.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Charge>()
                .HasOne(c => c.Contract)
                .WithMany()
                .HasForeignKey(c => c.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.IncidentReport)
                .WithMany()
                .HasForeignKey("CurrentIncidentReportId")
                .OnDelete(DeleteBehavior.Restrict);







        }



    }
}
