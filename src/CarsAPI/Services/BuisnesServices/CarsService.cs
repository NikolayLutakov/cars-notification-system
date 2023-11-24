using CarsAPI.Constants;
using CarsAPI.Enums;
using CarsAPI.Models.CarServiceModels;
using CarsAPI.Models.DataModels.Output;
using CarsAPI.Services.Interfaces;
using System.Text;

namespace CarsAPI.Services.BuisnesServices
{
    public class CarsService : ICarsService
    {
        private readonly ICarsDataService carsDataService;
        private readonly ILogger logger;
        private readonly ITelegramMessageService telegramService;
        public CarsService(ILogger<CarsService> logger, ICarsDataService carsDataService, ITelegramMessageService telegramService)
        {
            this.carsDataService = carsDataService;
            this.logger = logger;
            this.telegramService = telegramService;
        }

        public async Task CheckCarsTaxesAsync()
        {
            var cars = this.carsDataService.GetCarsInfos();

            foreach (var car in cars)
            {
                await this.ProcessCarInfo(car);
            }
        }

        private async Task ProcessCarInfo(CarInfoModel car)
        {
            var results = new List<CheckTaxesResult>();

            var tollResult = this.CheckToll(car.TollTaxes);

            if (tollResult.Data != null)
                results.Add(tollResult);

            var insuranceResult = this.CheckInshurance(car.CivilInsurances);

            if(insuranceResult.Data != null)
                results.Add(insuranceResult);

            var technicalInspectionResult = this.CheckTechnicalInspection(car.TechnicalInspections);
            
            if (technicalInspectionResult.Data != null)
                results.Add(technicalInspectionResult);

            if(results.Any())
                await this.SendNotificationMessage(results, car.RegistrationPlates, car.Owner);
        }

        private CheckTaxesResult CheckToll(ICollection<TollTaxInfoModel> tollTaxes)
        {
            var result = new CheckTaxesResult(CheckedItemsEnum.Toll);

            var sortedTaxes = tollTaxes.Where(x => x.IsActive).OrderByDescending(x => x.EndDate);

            if (!sortedTaxes.Any())
                return result;

            var lastTax = sortedTaxes.First();

            var now = DateTime.UtcNow;

            var daysDiff = (int)Math.Round((lastTax.EndDate - now).TotalDays);

            if(daysDiff <= 7)
            {

                var resultData = new CheckResultData()
                {
                    DueDate = lastTax.EndDate,
                    DaysLeft = daysDiff,
                };

                result.Data = resultData;
            }

            return result;
        }

        private CheckTaxesResult CheckInshurance(ICollection<CivilInsuranceInfoModel> insurances)
        {
            var result = new CheckTaxesResult(CheckedItemsEnum.Insurance);

            var sortedInsurances = insurances.Where(x => x.IsActive).OrderByDescending(x => x.EndDate);

            if (!sortedInsurances.Any())
                return result;

            var now = DateTime.UtcNow;

            var lastInsurance = sortedInsurances.First();

            var notPayedPremiums = lastInsurance.Premiums.Where(x => !x.IsPayed).OrderBy(x => x.DateOfPayment);

            if (notPayedPremiums.Any())
            {
                var upcomingPremium = notPayedPremiums.First();

                var daysDiff = (int)Math.Round((upcomingPremium.DateOfPayment - now).TotalDays);

                if(daysDiff <= 7)
                {
                    var resultData = new CheckResultData()
                    {
                        DueDate = upcomingPremium.DateOfPayment,
                        DaysLeft = daysDiff,
                        IsPremium = true,
                        PaymentPrice = upcomingPremium.PaymentPrice
                    };

                    result.Data = resultData;
                }
            }
            else
            {
                var daysDiff = (int)Math.Round((lastInsurance.EndDate - now).TotalDays);

                if (daysDiff <= 7)
                {
                    var resultData = new CheckResultData()
                    {
                        DueDate = lastInsurance.EndDate,
                        DaysLeft = daysDiff,
                    };

                    result.Data = resultData;
                }
            }

            return result;
        }

        private CheckTaxesResult CheckTechnicalInspection(ICollection<TechnicalInspectionInfoModel> technicalInspections)
        {
            var result = new CheckTaxesResult(CheckedItemsEnum.TechnicalInspection);

            var sortedTechnicalInspections = technicalInspections.Where(x => x.IsActive).OrderByDescending(x => x.EndDate);

            if (!sortedTechnicalInspections.Any())
                return result;

            var lastInspection = sortedTechnicalInspections.First();

            var now = DateTime.UtcNow;

            var daysDiff = (int)Math.Round((lastInspection.EndDate - now).TotalDays);

            if (daysDiff <= 7)
            {
                var resultData = new CheckResultData()
                {
                    DueDate = lastInspection.EndDate,
                    DaysLeft = daysDiff,
                };

                result.Data = resultData;
            }

            return result;
        }

        private async Task SendNotificationMessage(List<CheckTaxesResult> results, string registrationPlates, OwnerInfoModel ownerInfo)
        {
            if (ownerInfo.TelegramChatId == null && ownerInfo.Email == null)
            {
                this.logger.LogError("No contact information for owner: {ownerName}", ownerInfo.Name);
                return;
            }

            var message = new StringBuilder();

            foreach (var result in results)
            {
                message.AppendLine(this.CreateMessageLine(result, registrationPlates));
            }

            if (ownerInfo.TelegramChatId != null)
            {
                await this.telegramService.SendMessage(message.ToString(), ownerInfo.TelegramChatId);
            }
        }

        private string CreateMessageLine(CheckTaxesResult result, string registrationPlates)
        {
            string? line;

            if (result.CheckedItem == CheckedItemsEnum.Insurance)
            {
                if (result.Data.IsPremium)
                {
                    line = string.Format(CarServiceConstants.InsurancePremiumMessageTemplate, result.Data.DueDate.Date, registrationPlates, result.Data.PaymentPrice, result.Data.DaysLeft);
                }
                else
                {
                    line = string.Format(CarServiceConstants.InsuranceMessageTemplate, registrationPlates, result.Data.DueDate.Date, result.Data.DaysLeft);
                }
            }
            else
            {
                string? label;

                if (result.CheckedItem == CheckedItemsEnum.TechnicalInspection)
                    label = CarServiceConstants.TechnicalInspectionLabel;
                else
                    label = CarServiceConstants.TollTaxLabel;

                line = string.Format(CarServiceConstants.ShortMessageTemplate, label, registrationPlates, result.Data.DueDate.Date, result.Data.DaysLeft);
            }

            return line;
        }
    }
}
