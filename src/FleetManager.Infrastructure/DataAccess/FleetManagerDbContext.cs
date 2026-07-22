using FleetManager.Domain.Entities;
using FleetManager.Domain.Services.LoggedUser;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess
{
    public class FleetManagerDbContext(DbContextOptions dbContextOptions, IHttpContextAccessor httpContextAccessor, ILoggedUser loggedUser) : DbContext(dbContextOptions)
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses {get ; set ;}
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<HistoryLog> HistoryLogs { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            var pendingHistory = new List<(AudiTableEntity entity, string action)>();

            if (httpContextAccessor.HttpContext is not null)
            {

                var auditableEntries = ChangeTracker.Entries<AudiTableEntity>()
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

            modelBuilder.Entity<Tenant>()
                .OwnsOne(t => t.DriverLicense);

            modelBuilder.Entity<Tenant>()
                .OwnsOne(t => t.Contact);

            modelBuilder.Entity<Tenant>()
                .OwnsOne(t => t.Cpf, cpf =>
                {
                    cpf.HasIndex(c => c.Number).IsUnique();
                });
        }



    }
}
