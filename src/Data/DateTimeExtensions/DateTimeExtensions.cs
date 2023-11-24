namespace Data.DateTimeExtensions
{
    public static class DateTimeExtensions
    {
        public static DateTime SpecifyUtcKind(this DateTime dateTime)
        {
            var result = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            return result;
        }
    }
}
