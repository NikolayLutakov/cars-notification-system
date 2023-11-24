using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsAPI.Models.DataModels.Output;
using CarsAPI.Services.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;

namespace CarsAPI.Services.DataServices
{
    public class CarsDataService : ICarsDataService
    {
        private readonly CarsContext context;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public CarsDataService(CarsContext context, ILogger<CarsDataService> logger, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        public List<CarInfoModel> GetCarsInfos()
        {
            var cars = this.context.Cars.AsSplitQuery();

            var result = this.mapper.ProjectTo<CarInfoModel>(cars).ToList();

            return result;
        }
    }
}
