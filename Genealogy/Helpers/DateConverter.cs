
using System;
using System.Globalization;

namespace Genealogy.Helpers
{
    static public class DateConverter
    {
        static public DateTime ConvertToRTS(DateTime dateTime)
        {
            DateTime utcDateTime = dateTime.ToUniversalTime();
            // Çîíà 'Russian Standard Time' íå ðàáîòàåò è âûçûâàåò èñêëþ÷åíèå â Centos, òàê êàê îòñóòñòâóåò
            // Äëÿ Centos - Europe/Moscow
            string nzTimeZoneKey = "Europe/Moscow";
            TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
            DateTime nzDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, nzTimeZone);
            return nzDateTime;
        }
        static public string ConvertToStandardString(DateTime value)
        {
            return value.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
        }
    }
}