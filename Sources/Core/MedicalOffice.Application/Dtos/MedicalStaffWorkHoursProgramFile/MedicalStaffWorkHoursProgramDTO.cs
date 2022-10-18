using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.UserWorkHoursProgramFile;

public class UserWorkHoursProgramDTO 
{
    public UserWorkHoursProgramDTO()
    {
    }

    public Guid UserId { get; set; }
    public int MaxAppointmentCount { get; set; }
    public List<StaffWorkHour> StaffWorkHours { get; set; } = new();
}

public class StaffWorkHour
{
    public WeekDay Day { get; set; }
    public string MorningStart { get; set; } = string.Empty;
    public string MorningEnd { get; set; } = string.Empty;
    public string EveningStart { get; set; } = string.Empty;
    public string EveningEnd { get; set; } = string.Empty;
}

