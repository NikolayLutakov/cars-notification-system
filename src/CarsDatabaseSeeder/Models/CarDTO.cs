namespace CarsDatabaseSeeder.Models
{
    public class CarDTO
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public string YearOfManifacturing { get; set; }

        public string RegistrationPlates { get; set; }

        public ICollection<CivilInsuranceDTO> CivilInsurances { get; set; } = new List<CivilInsuranceDTO>();

        public ICollection<TechnicalInspectionDTO> TechnicalInspections { get; set; } = new List<TechnicalInspectionDTO>();

        public ICollection<TollTaxDTO> TollTaxes { get; set; } = new List<TollTaxDTO>();

        public ICollection<OilChangeDTO> OilChanges { get; set; } = new List<OilChangeDTO>();

        public ICollection<GearingChangeDTO> GearingChanges { get; set; } = new List<GearingChangeDTO>();
    }
}
