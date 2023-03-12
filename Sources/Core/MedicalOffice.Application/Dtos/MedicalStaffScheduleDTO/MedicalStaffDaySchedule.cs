namespace MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;

public class MedicalStaffDaySchedule
{
    public DayOfWeek WeekDay { get; set; }

    public string? MorningStart { get; set; }

    public string? MorningEnd { get; set; }

    public string? EveningStart { get; set; }

    public string? EveningEnd { get; set; }
}

