namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class SearchAppointmentsDto
    {
        public DateTime? Date { get; set; }
        public List<FilterFields> FilterFields { get; set; }
    }

    public class FilterFields
    {
        public Guid MedicalStaffId { get; set; }
        public Guid SectionId { get; set; }
        public Guid RoomId { get; set; }
    }
}