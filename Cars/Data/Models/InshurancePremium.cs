using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class InshurancePremium
    {
        public int Id { get; set; }

        public DateTime DateOfPaynment { get; set; }

        [Required]
        public decimal PaynmentPrice { get; set; }

        public bool IsPayed { get; set; }

        public virtual CivilInshurance Inshurance { get; set; }
    }
}
