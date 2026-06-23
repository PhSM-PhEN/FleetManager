using FleetManager.Domain.Entities;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Infrastructure.Services.LoggedUser;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess
{
    public class FleetManagerDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor, ILoggedUser loggedUser) : DbContext(options)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ILoggedUser _loggedUser = loggedUser;
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses {get; set;}
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Rental> Rentals {get ; set; }
        public DbSet<RentalPlan> RentalPlans { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            if(_httpContextAccessor.HttpContext is not null)
            {
                var user = await _loggedUser.Get();
                foreach(var entry in ChangeTracker.Entries<AuditableEntity>())
                {
                    if(entry.State == EntityState.Added)
                        entry.Entity.SetCreatedBy(user.Id);
                        
                    if(entry.State == EntityState.Modified)
                        entry.Entity.SetUpdatedBy(user.Id);
                }
            }
            return await base.SaveChangesAsync(ct);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.CPF).IsUnique();

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.CnhRegisterNumber).IsUnique();

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Cnpj).IsUnique();

            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.LicensePlate).IsUnique();

            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.Renavam).IsUnique();

            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.ChassisNumber).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserIdentifier).IsUnique();
        }
    }
}
