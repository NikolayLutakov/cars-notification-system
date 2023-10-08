using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsDatabaseSeeder.Services
{
    public class DatabaseService
    {
        private readonly CarsContext context;
        public DatabaseService()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CarsContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=CarsDatabase;User ID=postgres;password=Qwerty;Pooling=true");

            this.context = new CarsContext(optionsBuilder.Options);
        }

        public bool ResetDatabase()
        {
            try
            {
                this.context.Database.EnsureDeleted();
                this.context.Database.Migrate();
                this.Seed();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }

            return true;
        }

        public bool DeleteDatabase()
        {
            try
            {
                this.context.Database.EnsureDeleted();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }

            return true;
        }

        public bool CreateDatabase()
        {
            try
            {
                this.context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }

            return true;
        }

        public bool SeedDatabase()
        {
            if (!this.context.Database.CanConnect())
            {
                Console.WriteLine("Database does not exist.");
                return false;
            }

            if(this.context.Cars.Any())
            {
                Console.WriteLine("Database already seeded.");
                return false;
            }

            try
            {
                this.Seed();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }

            return true;
        }

        private void Seed()
        {
            var oner = new Owner()
            {
                Name = "Test Owner",
                TelegramChatId = "",
            };

            var car = new Car()
            {
                Make = "Lada",
                Model = "Niva",
                RegistrationPlates = "XX1234XX",
                Owner = oner,
            };

            var technicalInspection = new TechnicalInspection()
            {
                StartDate = DateTime.UtcNow.AddDays(10),
                EndDate = DateTime.UtcNow.AddDays(3),
                Car = car,
            };

            this.context.Add(technicalInspection);
            this.context.SaveChanges();
        }
    }
}
