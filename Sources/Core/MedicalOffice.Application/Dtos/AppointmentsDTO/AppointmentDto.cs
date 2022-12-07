using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos
{
    public class AppointmentDto
    {
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