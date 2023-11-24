namespace CarsAPI.Models.DataModels.Output
{
    public class InsurancePremumInfoModel
    {
        public DateTime DateOfPayment { get; set; }

        public decimal PaymentPrice { get; set; }

        public bool IsPayed => DateTime.UtcNow > DateOfPayment;
    }
}
