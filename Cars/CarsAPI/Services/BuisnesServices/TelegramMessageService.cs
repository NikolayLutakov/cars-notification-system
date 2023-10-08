﻿using CarsAPI.Constants;
using CarsAPI.Services.Interfaces;
using System.Runtime.CompilerServices;

namespace CarsAPI.Services.BuisnesServices
{
    public class TelegramMessageService : ITelegramMessageService
    {
        private readonly ILogger logger;
        private readonly IHttpClientFactory httpClientFactory;

        public TelegramMessageService(IHttpClientFactory httpClientFactory, ILogger<TelegramMessageService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task SendMessage(string message, string chatId)
        {
            var botKey = "";

            var address = string.Format(CarServiceConstants.NotificationAddressTemplate, botKey, chatId, message);

            var httpMessage = new HttpRequestMessage(HttpMethod.Get, address);

            var client = this.httpClientFactory.CreateClient();

            var response = await client.SendAsync(httpMessage);

            if (response.IsSuccessStatusCode)
                this.logger.LogInformation("'{message}' successfully sent to {chat}", message, chatId);
            else
                this.logger.LogInformation("Failed to send '{message}' to {chat}", message, chatId);
        }
    }
}
