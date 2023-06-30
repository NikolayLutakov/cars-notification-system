namespace Data.Models
{
    public class TollTax
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public virtual Car Car { get; set; }
    }
}
