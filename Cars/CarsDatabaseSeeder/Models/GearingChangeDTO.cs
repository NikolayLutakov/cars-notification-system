using Data.Enums;

namespace CarsDatabaseSeeder.Models
{
    public class GearingChangeDTO
    {
        public int Mileage { get; set; }

        public int? NextChangeMileage { get; set; }

        public int Type { get; set; }
    }
}
