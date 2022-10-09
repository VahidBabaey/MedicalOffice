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

        var format = $"{year}/{month}/{day}";

        return format;
    }
}
