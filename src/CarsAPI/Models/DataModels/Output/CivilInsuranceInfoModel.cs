namespace CarsAPI.Models.DataModels.Output
{
    public class CivilInsuranceInfoModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public string InsuranceCompany { get; set; }

        public bool IsActive => DateTime.UtcNow < EndDate;

        public ICollection<InsurancePremumInfoModel> Premiums { get; set; }
    }
}
