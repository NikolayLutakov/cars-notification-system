using AutoMapper;
using CarsAPI.Models.DataModels.Output;
using Data.Models;

namespace CarsAPI.AutoMapper
{
    public class CarsProfile : Profile
    {
        public CarsProfile()
        {
            this.CreateMap<Car, CarInfoModel>();

            this.CreateMap<CivilInsurance, CivilInsuranceInfoModel>();

            this.CreateMap<InsurancePremium, InsurancePremumInfoModel>();

            this.CreateMap<Owner, OwnerInfoModel>();

            this.CreateMap<TechnicalInspection, TechnicalInspectionInfoModel>();

            this.CreateMap<TollTax, TollTaxInfoModel>();
        }
    }
}
