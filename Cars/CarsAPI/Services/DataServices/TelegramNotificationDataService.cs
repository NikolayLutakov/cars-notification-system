using AutoMapper;
using CarsAPI.Services.Interfaces;
using Data;

namespace CarsAPI.Services.DataServices
{
    public class TelegramNotificationDataService : ITelegramNotificationDataService
    {
        private readonly CarsContext context;

        public TelegramNotificationDataService(CarsContext context)
        {
            this.context = context;
        }

        public string GetBotKey()
        {
            //Only one bot in db.

            var key = this.context.TelegramBots.OrderBy(x => x.Id).FirstOrDefault().BotKey;

            var result = $"bot{key}";

            return result;
        }
    }
}
