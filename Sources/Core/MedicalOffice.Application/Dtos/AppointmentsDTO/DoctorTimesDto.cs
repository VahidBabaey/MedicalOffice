namespace MedicalOffice.WebApi.WebApi.Controllers
{
    public class DoctorTimesDTO
    {
        public Guid MedicalStaffId { get; set; }
        public Guid? ServiceId { get; set; }
        public Guid? RoomId { get; set; }
    }
}