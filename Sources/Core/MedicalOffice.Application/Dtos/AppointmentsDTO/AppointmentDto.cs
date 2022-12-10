using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class AppointmentDTO : IPhoneNumberDTO, INationalIdDTO
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string NationalID { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public DateTime date { get; set; }

        public Guid MedicalStaffId { get; set; }

        public Guid ServiceId { get; set; }

        public Guid SectionId { get; set; }

        public Guid? ReferrerId { get; set; }

        public AppointmentType? Status { get; set; } = AppointmentType.Approved;
    }
}