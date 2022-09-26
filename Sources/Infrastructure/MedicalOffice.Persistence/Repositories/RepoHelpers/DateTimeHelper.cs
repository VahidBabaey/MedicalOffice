using System.Globalization;

namespace MedicalOffice.Persistence.Repositories.RepoHelpers;

public static class DateTimeHelper
{
    public static string ToPersianDate(this DateTime dateTime)
    {
        PersianCalendar calendar = new();

        var year = calendar.GetYear(dateTime);
        var month = calendar.GetMonth(dateTime);
        var day = calendar.GetDayOfMonth(dateTime);

        return $"{year}/{month}/{day}";
    }

    public static string ToPersianTime(this DateTime dateTime)
    {
        PersianCalendar calendar = new();

        var hour = calendar.GetHour(dateTime);
        var minute = calendar.GetMinute(dateTime);

        return $"{hour}:{minute}";
    }

    public static string ToPersianDateTime(this DateTime dateTime)
    {
        var datePart = dateTime.ToPersianDate();
        var timePart = dateTime.ToPersianTime();

        return $"{datePart} - {timePart}";
    }
}
