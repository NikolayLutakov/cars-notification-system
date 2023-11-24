using CarsAPI.Enums;

namespace CarsAPI.Models.CarServiceModels
{
    public class CheckTaxesResult
    {
        public CheckTaxesResult(CheckedItemsEnum checkedItem)
        {
            this.CheckedItem = checkedItem;
        }
        public CheckedItemsEnum CheckedItem { get; }

        public CheckResultData? Data { get; set; }
    }

    public class CheckResultData
    {
        public int DaysLeft { get; set; }

        public DateTime DueDate { get; set; }

        public decimal? PaymentPrice { get; set; }

        public bool IsPremium { get; set; } = false;
    }
}
