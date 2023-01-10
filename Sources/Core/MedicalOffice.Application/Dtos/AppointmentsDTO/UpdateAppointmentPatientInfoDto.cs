using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class UpdateAppointmentPatientInfoDto : IAppointmentIdDTO, IPhoneNumberDTO, INationalIdDTO
    {
        public Guid AppointmentId { get; set; }

        public string PatientName { get; set; }

        public string PatientLastName { get; set; }

        public string PhoneNumber { get; set; }

        public string NationalID { get; set; }

        public Guid? ReferrerId { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}