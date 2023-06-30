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

        public virtual DbSet<Car> Cars { get; set; }

        public virtual DbSet<CivilInshurance> CivilInshurances { get; set; }

        public virtual DbSet<GearingChange> GearingChanges { get; set; }

        public virtual DbSet<InshurancePremium> InshurancePremiums { get; set; }

        public virtual DbSet<OilChange> OilChanges { get; set; }

        public virtual DbSet<TechnicalInspection> TechnicalInspections { get; set; }

        public virtual DbSet<TollTax> TollTaxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(car =>
            {
                car.HasIndex(x => x.RegistrationPlates).IsUnique();
            }); 
        }

    }
}
