using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AutoMapper.Resolver
{
    public static class DateTimeHoursFormatResolve
    {
        public static string YearMonthDayHoursMinutes(DateTime? date)
        {
            return date != null ? date.Value.ToString("yyyy-MM-dd HH:mm") : null;
        }
    }
}
