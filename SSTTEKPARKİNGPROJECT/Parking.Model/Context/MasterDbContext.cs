using Microsoft.EntityFrameworkCore;
using Parking.Model.Entities;
using Parking.Model.Enums;

namespace Parking.Model.Context
{
    public class MasterDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSqlServer("Server=FORTHECODE;Database=SSTEKCARPARKDB;Trusted_Connection=True;TrustServerCertificate=True;");

        }

        public DbSet<CarPark> CarParks { get; set; }
        public DbSet<FirstClassVehicle> FirstClassVehicle { get; set; }
        public DbSet<SecondClassVehicle> SecondClassVehicle { get; set; }
        public DbSet<ThirdClassVehicle> ThirdClassVehicle { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<WashedVehicle> WashedVehicle { get; set; }
        public DbSet<TyreChangedVehicle> TyreChangedVehicle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<Vehicle>()
                .HasDiscriminator<string>("CarParkType")
                .HasValue<FirstClassVehicle>("FirstClass")
                .HasValue<SecondClassVehicle>("SecondClass")
                .HasValue<ThirdClassVehicle>("ThirdClass");

            modelBuilder.Entity<CarPark>()
                .HasData(
                    new CarPark { Id = 1, IsOpen = true }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
