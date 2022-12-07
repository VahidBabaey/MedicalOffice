namespace MedicalOffice.WebApi.WebApi.Controllers
{
    public class DoctorTimesDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid MedicalStaffId { get; set; }
        public Guid? ServiceId { get; set; }
        public Guid? RoomId { get; set; }
    }
}