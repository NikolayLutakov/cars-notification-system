using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class CivilInshurance
    {
        public CivilInshurance()
        {
            this.Premiums = new List<InshurancePremium>();
        }

        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(50)]
        public string InshuranceCompany { get; set; }

        public bool IsActive { get; set; }

        public virtual Car Car { get; set; }

        public virtual ICollection<InshurancePremium> Premiums { get; set; }
    }
}
