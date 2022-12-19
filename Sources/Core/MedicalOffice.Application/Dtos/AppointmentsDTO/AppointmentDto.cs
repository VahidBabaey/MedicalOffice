using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class AppointmentDTO : IPhoneNumberDTO, INationalIdDTO
    {
        public string PatientName { get; set; }

        public string PatientLastName { get; set; }

        public string PhoneNumber { get; set; }

        public string NationalID { get; set; }
        
        public DateTime Date { get; set; }

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        public Guid MedicalStaffId { get; set; }

        public Guid ServiceId { get; set; }

        public Guid? RoomId { get; set; }

        public Guid? DeviceId { get; set; }

        public Guid? ReferrerId { get; set; }

        public AppointmentType AppointmentType{ get; set; }
    }
}