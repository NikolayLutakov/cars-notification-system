using Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CarsAPI.Models.DataModels.Output
{
    public class CarInfoModel
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public DateTime YearOfManifacturing { get; set; }

        public string RegistrationPlates { get; set; }

        public ICollection<CivilInsuranceInfoModel> CivilInsurances { get; set; }

        public ICollection<TechnicalInspectionInfoModel> TechnicalInspections { get; set; }

        public ICollection<TollTaxInfoModel> TollTaxes { get; set; }

        public OwnerInfoModel Owner { get; set; }
    }
}
