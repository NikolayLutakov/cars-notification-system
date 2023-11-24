namespace CarsAPI.Services.Interfaces
{
    public interface ITelegramMessageService
    {
        Task SendMessage(string message, string chatId);
    }
}
