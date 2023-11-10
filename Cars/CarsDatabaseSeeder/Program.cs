using CarsDatabaseSeeder.Services;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace CarsDatabaseSeeder
{
    public class Program
    {
        public static JObject Configs { get; private set; }
        static void Main(string[] args)
        {
            LoadConfigs();

            var engine = new Engine();

            engine.Start();
        }

        private static void LoadConfigs()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "CarsDatabaseSeeder.config.json";

            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            using StreamReader reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            JObject config = JObject.Parse(json);

            Configs = config;
        }
    }
}