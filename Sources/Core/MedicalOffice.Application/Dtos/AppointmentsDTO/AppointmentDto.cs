using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class AppointmentDto
    {
        public string StaffName { get; set; }
        public string StaffLastName { get; set; }
        public string CreatorName { get; set; }
        public string CreatorLastName { get; set; }
        public string ServiceName { get; set; }
        public string Time { get; set; }
        public string PatientName { get; set; }
        public string PatientLastName { get; set; }
        public string PhoneNumber { get; set; }
        public AppointmentType status { get; set; }
    }
}