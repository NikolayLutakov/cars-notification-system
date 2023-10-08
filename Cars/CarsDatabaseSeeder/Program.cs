using CarsDatabaseSeeder.Services;

namespace CarsDatabaseSeeder
{
    public class Program
    {
        static void Main(string[] args)
        {
            var engine = new Engine();

            engine.Start();
        }
    }
}