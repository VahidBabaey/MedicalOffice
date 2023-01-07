namespace MedicalOffice.WebApi.WebApi.Controllers
{
    public class GetSpecificPeriodAppointmentDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? MedicalStaffId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid? RoomId { get; set; }
        public Guid? DeviceId{ get; set; }
    }
}