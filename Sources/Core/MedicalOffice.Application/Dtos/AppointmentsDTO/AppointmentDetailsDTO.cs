using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos
{
    public class AppointmentDetailsDTO
    {
        public Guid MedicalStaffId{ get; set; }

        public DateTime Date{ get; set; }

        public string StaffName { get; set; }

        public string? CreatorName { get; set; }

        public string? CreatorLastName { get; set; }

        public string? ServiceName { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string? PatientName { get; set; }

        public string? PatientLastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? NationalID { get; set; }

        public string DeviceName { get; set; }

        public string RoomName { get; set; }

        public AppointmentType? AppointmentType { get; set; }
    }
}