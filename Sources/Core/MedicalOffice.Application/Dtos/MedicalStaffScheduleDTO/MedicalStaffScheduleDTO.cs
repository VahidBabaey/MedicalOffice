using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;

public class MedicalStaffScheduleDTO
{
    public Guid MedicalStaffId { get; set; }

    public int MaxAppointmentCount { get; set; }

    public List<MedicalStaffDaySchedule> MedicalStaffSchedule { get; set; } = new();
}

public class MedicalStaffDaySchedule
{
    public WeekDay WeekDay { get; set; }

    public string MorningStart { get; set; }

    public string MorningEnd { get; set; }

    public string EveningStart { get; set; }

    public string EveningEnd { get; set; }
}

