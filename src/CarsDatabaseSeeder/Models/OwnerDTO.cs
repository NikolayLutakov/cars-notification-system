namespace CarsDatabaseSeeder.Models
{
    public class OwnerDTO
    {
        public string Name { get; set; }

        public string? Email { get; set; }

        public string? TelegramChatId { get; set; }

        public ICollection<CarDTO> Cars { get; set; }
    }
}