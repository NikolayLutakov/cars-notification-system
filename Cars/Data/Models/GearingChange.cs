using Data.Enums;

namespace Data.Models
{
    public class GearingChange
    {
        public int Id { get; set; }

        public int Mileage { get; set; }

        public int? NextChangeMileage { get; set; }

        public GearingType Type { get; set; }

        public Car Car { get; set; }
    }
}
