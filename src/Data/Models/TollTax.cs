namespace Data.Models
{
    public class TollTax
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Car Car { get; set; }
    }
}
