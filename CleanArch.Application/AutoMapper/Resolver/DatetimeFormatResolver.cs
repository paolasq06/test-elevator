using System;

namespace Application.AutoMapper.Resolver
{
    public static class DatetimeFormatResolver
    {
        public static string YearMonthDay(DateTime? date)
        {
            return date != null ? date.Value.ToString("yyyy-MM-dd"):null;
        }

        public static string YearMonthDayHoursMinutes(DateTime? date)
        {
            return date != null ? date.Value.ToString("yyyy-MM-dd HH:mm") : null;
        }
    }
}


