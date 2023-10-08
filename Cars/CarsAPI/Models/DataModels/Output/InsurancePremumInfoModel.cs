namespace CarsAPI.Models.DataModels.Output
{
    public class InsurancePremumInfoModel
    {
        public DateTime DateOfPaynment { get; set; }

        public decimal PaynmentPrice { get; set; }

        public bool IsPayed => DateTime.UtcNow < DateOfPaynment;
    }
}
