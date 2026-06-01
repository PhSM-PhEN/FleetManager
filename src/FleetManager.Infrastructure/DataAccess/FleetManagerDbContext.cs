using FleetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess
{
    public class FleetManagerDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses {get; set;}
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Rental> Rentals {get ; set; }
        public DbSet<RentalPlan> RentalPlans { get; set; }

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
