using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class InsurancePremium
    {
        public int Id { get; set; }

        public DateTime DateOfPaynment { get; set; }

        public decimal PaynmentPrice { get; set; }

        public CivilInsurance Insurance { get; set; }
    }
}
