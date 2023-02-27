using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;

public class MedicalStaffScheduleDTO : IMedicalStaffDTO
{
    public Guid MedicalStaffId { get; set; }

    public int MaxAppointmentCount { get; set; }

    public List<MedicalStaffDaySchedule> MedicalStaffSchedule { get; set; } = new();
}