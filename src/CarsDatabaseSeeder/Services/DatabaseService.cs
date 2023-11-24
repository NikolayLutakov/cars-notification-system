using CarsDatabaseSeeder.Models;
using Data;
using Data.Enums;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CarsDatabaseSeeder.Services
{
    public class DatabaseService
    {
        private readonly CarsContext context;
        public DatabaseService()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CarsContext>();

            var connectionString = this.ReadConnectionString();

            optionsBuilder.UseNpgsql(connectionString);

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
            var botKey = this.ReadBotKeyFile();

            var bot = new TelegramBot()
            {
                BotKey = botKey,
                Name = "Telegram Notifier Bot",
            };

            var ownersDTOs = this.ReadUsersConfigFile();

            var ownersToAdd = new List<Owner>();

            foreach (var ownerDTO in ownersDTOs)
            {
                var owner = new Owner()
                {
                    Name = ownerDTO.Name,
                    Email = ownerDTO.Email,
                    TelegramChatId = ownerDTO.TelegramChatId,
                    Cars = ownerDTO.Cars.Select(carDTO => new Car()
                    {
                        Make = carDTO.Make,
                        Model = carDTO.Model,
                        RegistrationPlates = carDTO.RegistrationPlates,
                        YearOfManifacturing = DateTime.SpecifyKind(DateTime.Parse(carDTO.YearOfManifacturing), DateTimeKind.Utc),
                        CivilInsurances = carDTO.CivilInsurances.Select(civilInsuranceDTO => new CivilInsurance()
                        {
                            InsuranceCompany = civilInsuranceDTO.InsuranceCompany,
                            Price = civilInsuranceDTO.Price,
                            StartDate = DateTime.SpecifyKind(DateTime.Parse(civilInsuranceDTO.StartDate), DateTimeKind.Utc),
                            EndDate = DateTime.SpecifyKind(DateTime.Parse(civilInsuranceDTO.EndDate), DateTimeKind.Utc),
                            Premiums = civilInsuranceDTO.Premiums.Select(premiumDTO => new InsurancePremium()
                            {
                                PaymentPrice = premiumDTO.PaymentPrice,
                                DateOfPayment = DateTime.SpecifyKind(DateTime.Parse(premiumDTO.DateOfPayment), DateTimeKind.Utc)
                            }).ToList()
                        }).ToList(),
                        GearingChanges = carDTO.GearingChanges.Select(gearingChangeDTO => new GearingChange()
                        {
                            Mileage = gearingChangeDTO.Mileage,
                            NextChangeMileage = gearingChangeDTO.NextChangeMileage,
                            Type = (GearingType)gearingChangeDTO.Type
                        }).ToList(),
                        OilChanges = carDTO.OilChanges.Select(oilChangeDTO => new OilChange()
                        {
                            OilMake = oilChangeDTO.OilMake,
                            NextChangeMileage = oilChangeDTO.NextChangeMileage,
                            ChangedOn = DateTime.SpecifyKind(DateTime.Parse(oilChangeDTO.ChangedOn), DateTimeKind.Utc),
                            Mileage = oilChangeDTO.Mileage,
                            OilType = oilChangeDTO.OilType
                        }).ToList(),
                        TechnicalInspections = carDTO.TechnicalInspections.Select(technicalInspectionDTO => new TechnicalInspection()
                        {
                            StartDate = DateTime.SpecifyKind(DateTime.Parse(technicalInspectionDTO.StartDate), DateTimeKind.Utc),
                            EndDate = DateTime.SpecifyKind(DateTime.Parse(technicalInspectionDTO.EndDate), DateTimeKind.Utc)
                        }).ToList(),
                        TollTaxes = carDTO.TollTaxes.Select(tollTaxDTO => new TollTax()
                        {
                            StartDate = DateTime.SpecifyKind(DateTime.Parse(tollTaxDTO.StartDate), DateTimeKind.Utc),
                            EndDate = DateTime.SpecifyKind(DateTime.Parse(tollTaxDTO.EndDate), DateTimeKind.Utc)
                        }).ToList(),
                    }).ToList(),
                };

                ownersToAdd.Add(owner);
            }

            this.context.AddRange(ownersToAdd);
            this.context.Add(bot);
            try
            {

                this.context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        private string ReadConnectionString()
        {
            var path = (string)Program.Configs["DbConnectionString"];

            var connectionString = File.ReadAllText(path);

            return connectionString;
        }

        private string ReadBotKeyFile()
        {
            var path = (string)Program.Configs["TelegramBotConfigFilePath"];

            var botKey = File.ReadAllText(path);

            return botKey;
        }

        private List<OwnerDTO> ReadUsersConfigFile()
        {
            var path = (string)Program.Configs["UsersFilePath"];

            var usersString = File.ReadAllText(path);

            var users = JsonConvert.DeserializeObject<List<OwnerDTO>>(usersString);

            return users;
        }
    }
}
