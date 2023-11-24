using CarsDatabaseSeeder.Enums;

namespace CarsDatabaseSeeder.Services
{
    public class Engine
    {
        public void Start()
        {
            var databaseService = new DatabaseService();

            this.WriteLegend();

            var input = this.ParseInput(Console.ReadLine());

            while (input != InputOptions.Exit)
            {

                bool result;

                switch (input)
                {
                    case InputOptions.Reset_Database:
                        result =databaseService.ResetDatabase();
                        break;
                    case InputOptions.Delete_Database:
                        result = databaseService.DeleteDatabase();
                        break;
                    case InputOptions.Create_Database:
                        result = databaseService.CreateDatabase();
                        break;
                    case InputOptions.Seed_Database:
                        result = databaseService.SeedDatabase();
                        break;
                    default:
                        return;
                }

                if (result)
                    Console.WriteLine($"{input} completed. Press any key to continue.");
                else
                    Console.WriteLine($"There was error while performing operation '{input}'. Press any key to continue.");

                Console.ReadKey();

                Console.Clear();

                this.WriteLegend();

                input = this.ParseInput(Console.ReadLine());
            }
        }

        private void WriteLegend()
        {
            Console.WriteLine($"[{(int)InputOptions.Reset_Database}] - Reset Database (Deletes the database applys migrations and seeds the database)");
            Console.WriteLine($"[{(int)InputOptions.Delete_Database}] - Delete Database (Only deletes the database)");
            Console.WriteLine($"[{(int)InputOptions.Create_Database}] - Create Database (Creates the database from latest migration)");
            Console.WriteLine($"[{(int)InputOptions.Seed_Database}] - Seed Database (Seeds the database with initial information)");
            Console.WriteLine($"[{(int)InputOptions.Exit}] - Exit");
        }

        private InputOptions ParseInput(string input)
        {
            var intParseResult = int.TryParse(input, out int inputInt);

            InputOptions result;
            InputOptions? handledResult = null;

            if (!intParseResult || !Enum.IsDefined(typeof(InputOptions), inputInt))
                handledResult = HandleInvalidInput();

            if (handledResult != null)
                result = (InputOptions)handledResult;
            else
                result = (InputOptions)inputInt;

            return result;
        }

        private InputOptions HandleInvalidInput()
        {
            Console.Clear();
            WriteLegend();
            Console.WriteLine($"Invalid input value! Pease enter number from {(int)InputOptions.Exit} to {(int)InputOptions.Seed_Database}");

            var newInput = Console.ReadLine();
            var result = ParseInput(newInput);

            return result;
        }
    }
}
