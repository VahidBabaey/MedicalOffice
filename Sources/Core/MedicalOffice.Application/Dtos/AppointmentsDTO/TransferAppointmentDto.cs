using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class UpdateAppointmentDTO : IAppointmentIdDTO
    {
        public Guid AppointmentId { get; set; }

        public Guid? MedicalStaffId { get; set; }

        public Guid? ServiceId { get; set; }

        public Guid? RoomId { get; set; }

        public Guid? DeviceId { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public DateTime Date { get; set; }

        public string? Description { get; set; }
    }
}