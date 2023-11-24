using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Car
    {
        public Car()
        {
            this.CivilInsurances = new List<CivilInsurance>();
            this.TechnicalInspections = new List<TechnicalInspection>();
            this.TollTaxes = new List<TollTax>();
            this.OilChanges = new List<OilChange>();
            this.GearingChanges = new List<GearingChange>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Make { get; set; }

        [Required]
        [MaxLength(30)]
        public string Model { get; set; }

        public DateTime YearOfManifacturing { get; set; }

        [Required]
        [MaxLength(15)]
        public string RegistrationPlates { get; set; }

        public ICollection<CivilInsurance> CivilInsurances { get; set; }

        public ICollection<TechnicalInspection> TechnicalInspections { get; set; }

        public ICollection<TollTax> TollTaxes { get; set; }

        public ICollection<OilChange> OilChanges { get; set; }

        public ICollection<GearingChange> GearingChanges { get; set; }
        
        public Owner Owner { get; set; }

    }
}
