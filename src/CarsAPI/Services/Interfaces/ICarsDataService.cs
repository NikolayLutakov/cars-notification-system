using CarsAPI.Models.DataModels.Output;

namespace CarsAPI.Services.Interfaces
{
    public interface ICarsDataService
    {
        List<CarInfoModel> GetCarsInfos();
    }
}
