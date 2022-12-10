using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO
namespace MedicalOffice.Application.Dtos
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
        public string Name { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string NationalId { get; set; }

        public Guid MedicalStaffId { get; set; }

        public Guid ServiceId { get; set; }

        public Guid SectionId { get; set; }

        public AppointmentType status { get; set; }
    }
}