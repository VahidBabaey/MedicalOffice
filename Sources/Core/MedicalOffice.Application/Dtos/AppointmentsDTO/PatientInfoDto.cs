namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class PatientInfoDTO
    {
        public Guid AppointmentId{ get; set; }

        public string PatientName { get; set; }

        public string PatientLastName { get; set; }
        
        public string PhoneNumber { get; set; }

        public string NationalID { get; set; }

        public string ReferrerId{ get; set; }

        public string Description { get; set; }
    }
}