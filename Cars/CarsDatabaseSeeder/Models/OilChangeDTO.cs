namespace CarsDatabaseSeeder.Models
{
    public class OilChangeDTO
    {
        public int Mileage { get; set; }

        public int NextChangeMileage { get; set; }

        public string OilMake { get; set; }

        public string OilType { get; set; }

        public string ChangedOn { get; set; }
    }
}
