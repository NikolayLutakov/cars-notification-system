namespace CarsAPI.Models.DataModels.Output
{
    public class TechnicalInspectionInfoModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive => DateTime.UtcNow < EndDate;
    }
}
