using CarsDatabaseSeeder.Enums;

namespace CarsDatabaseSeeder.Services
{
    public class Engine
    {
        public void Start()
        {
            this.WriteLegend();

            while (this.ParseInput(Console.ReadLine()) != InputOptions.Exit)
            {
                Console.WriteLine("Processing...");
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
            var reslt = ParseInput(newInput);

            return reslt;
        }
    }
}
