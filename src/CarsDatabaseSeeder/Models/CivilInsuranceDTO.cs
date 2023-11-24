namespace CarsDatabaseSeeder.Models
{
    public class CivilInsuranceDTO
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public decimal Price { get; set; }

        public string InsuranceCompany { get; set; }

        public ICollection<InsurancePremiumDTO> Premiums { get; set; } = new List<InsurancePremiumDTO>();
    }
}
