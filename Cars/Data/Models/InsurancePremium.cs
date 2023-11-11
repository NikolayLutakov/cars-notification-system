using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class InsurancePremium
    {
        public int Id { get; set; }

        public DateTime DateOfPayment { get; set; }

        public decimal PaymentPrice { get; set; }

        public CivilInsurance Insurance { get; set; }
    }
}
