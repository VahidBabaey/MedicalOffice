namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class TransferAppointmentDto
    {
        public Guid AppointmentId { get; set; }
        public Guid ServicId { get; set; }
        public Guid RoomId { get; set; }
        public Guid MedicalStaffId { get; set; }
        public string Description{ get; set; }
    }
}