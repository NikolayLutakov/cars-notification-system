using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class CarsContext : DbContext
    {
        public CarsContext(DbContextOptions<CarsContext> options)
        : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CivilInsurance> CivilInsurances { get; set; }

        public DbSet<GearingChange> GearingChanges { get; set; }

        public DbSet<InsurancePremium> InshurancePremiums { get; set; }

        public DbSet<OilChange> OilChanges { get; set; }

        public DbSet<TechnicalInspection> TechnicalInspections { get; set; }

        public DbSet<TollTax> TollTaxes { get; set; }

        public DbSet<TelegramBot> TelegramBots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(car =>
            {
                car.HasIndex(x => x.RegistrationPlates).IsUnique();
            }); 
        }

    }
}
