using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFileDTO;

public class MedicalStaffWorkHoursProgramDTO 
{
    public Guid MedicalStaffId { get; set; }

    public int MaxAppointmentCount { get; set; }

    public List<MedicalStaffWorkHour> MedicalStaffWorkHours { get; set; } = new();
}

public class MedicalStaffWorkHour
{
    public WeekDay WeekDay { get; set; }

    public TimeOnly MorningStart { get; set; } = TimeOnly.MinValue;

    public TimeOnly MorningEnd { get; set; } = TimeOnly.MinValue;

    public TimeOnly EveningStart { get; set; } = TimeOnly.MinValue;

    public TimeOnly EveningEnd { get; set; } = TimeOnly.MinValue;
}

