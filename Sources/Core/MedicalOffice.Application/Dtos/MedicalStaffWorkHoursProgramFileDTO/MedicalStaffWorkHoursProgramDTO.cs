using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFileDTO;

public class MedicalStaffWorkHoursProgramDTO 
{
    public MedicalStaffWorkHoursProgramDTO()
    {
    }

    public Guid MedicalStaffId { get; set; }
    public int MaxAppointmentCount { get; set; }
    public List<MedicalStaffWorkHour> StaffWorkHours { get; set; } = new();
}

public class MedicalStaffWorkHour
{
    public WeekDay Day { get; set; }
    public string MorningStart { get; set; } = string.Empty;
    public string MorningEnd { get; set; } = string.Empty;
    public string EveningStart { get; set; } = string.Empty;
    public string EveningEnd { get; set; } = string.Empty;
}

