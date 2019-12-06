using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionParcMachinerieTP3.DateTimeHelper
{

    public class DateTimeHelper
    {
        static DateTime unixOrigin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime LongToDateTime(long timestamp)
        {
            DateTime start = unixOrigin;
            return start.AddSeconds(timestamp);
        }

        public static long DateTimeToLong(DateTime date)
        {
            TimeSpan diff = date.ToUniversalTime() - unixOrigin;
            return (long)Math.Floor(diff.TotalSeconds);
        }
        public static TimeSpan LongDiff(long from, long to)
        {
            TimeSpan diff = LongToDateTime(to) - LongToDateTime(from);
            return diff;
        }
    }
}