using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class CivilInsurance
    {
        public CivilInsurance()
        {
            this.Premiums = new List<InsurancePremium>();
        }

        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(50)]
        public string InsuranceCompany { get; set; }

        public Car Car { get; set; }

        public ICollection<InsurancePremium> Premiums { get; set; }
    }
}
