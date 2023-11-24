namespace CarsAPI.Models.DataModels.Output
{
    public class TollTaxInfoModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive => DateTime.UtcNow < EndDate;
    }
}
