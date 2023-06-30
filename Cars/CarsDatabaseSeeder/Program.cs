using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsDatabaseSeeder
{
    public class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CarsContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=CarsDatabase;User ID=postgres;password=Qwerty;Pooling=true", x => x.MigrationsAssembly("Data"));

            using var context = new CarsContext(optionsBuilder.Options);

            var car = new Car()
            {
                Make = "Opel",
                Model = "Vecta",
                YearOfManifacturing = new DateTime(1996, 1, 1),
                RegistrationPlates = "XX1234AB",         
            };

            context.Cars.Add(car);
            context.SaveChanges();
        }
    }
}