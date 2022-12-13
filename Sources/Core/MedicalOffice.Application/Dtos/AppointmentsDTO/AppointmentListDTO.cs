using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos
{
    public class AppointmentDetailsDTO
    {
        public string StaffName { get; set; }

        public string StaffLastName { get; set; }

        public string? CreatorName { get; set; }

        public string? CreatorLastName { get; set; }

        public string? ServiceName { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string? PatientName { get; set; }

        public string? PatientLastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? NationalID { get; set; }

        public AppointmentType? status { get; set; }
    }
}