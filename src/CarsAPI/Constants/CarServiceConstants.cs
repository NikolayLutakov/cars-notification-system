namespace CarsAPI.Constants
{
    public class CarServiceConstants
    {
        public const string TechnicalInspectionLabel = "Технически преглед";

        public const string TollTaxLabel = "Винетка"; 

        public const string ShortMessageTemplate = "{0} на автомобил с регистрационен номер {1} изтича на {2}. Остават {3} дни.";

        public const string InsuranceMessageTemplate = "Гражданска отговорност на автомобил с регистрационен номер {0} изтича на {1}. Остават {2} дни.";

        public const string InsurancePremiumMessageTemplate = "На {0} предстои вноска по гражданска отговорност на автомобил с регистрационен номер {1} на стойност {2} лвева. Остават {3} дни.";

        public const string NotificationAddressTemplate = "https://api.telegram.org/{0}/sendMessage?chat_id={1}&text={2}";
    }
}
