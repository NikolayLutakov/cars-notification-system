using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class OilChange
    {
        public int Id { get; set; }

        public int Mileage { get; set; }
      
        public int NextChangeMileage { get; set; }

        [Required]
        public string OilMake { get; set; }

        [Required]
        public string OilType { get; set; }

        public DateTime ChangedOn { get; set; }
    }
}
