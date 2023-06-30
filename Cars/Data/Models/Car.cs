using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Car
    {
        public Car()
        {
            this.CivilInshurances = new List<CivilInshurance>();
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
        public string RegistrationPlates { get; set; }

        public virtual ICollection<CivilInshurance> CivilInshurances { get; set; }

        public virtual ICollection<TechnicalInspection> TechnicalInspections { get; set; }

        public virtual ICollection<TollTax> TollTaxes { get; set; }

        public virtual ICollection<OilChange> OilChanges { get; set; }

        public virtual ICollection<GearingChange> GearingChanges { get; set; }

    }
}
