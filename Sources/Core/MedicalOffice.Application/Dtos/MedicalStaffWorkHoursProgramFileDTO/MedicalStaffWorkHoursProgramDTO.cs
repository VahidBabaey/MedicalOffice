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

    public string MorningStart { get; set; }

    public string MorningEnd { get; set; }

    public string EveningStart { get; set; }

    public string EveningEnd { get; set; }
}

