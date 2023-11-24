using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Owner
    {
        public Owner()
        {
            this.Cars = new List<Car>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(50)]
        public string? TelegramChatId { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
