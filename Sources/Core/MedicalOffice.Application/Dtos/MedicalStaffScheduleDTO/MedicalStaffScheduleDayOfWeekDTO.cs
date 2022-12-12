namespace MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO
{
    public class MedicalStaffScheduleDayOfWeekDTO
    {
        public Guid MedicalStaffId { get; set; }

        public string StaffName{ get; set; }

        public string StaffLastName { get; set; }

        public DayOfWeek DayOfWeek{ get; set; }

        public int MaxAppointmentCount { get; set; }

        public string MorningStart { get; set; }

        public string MorningEnd { get; set; }

        public string EveningStart { get; set; }

        public string EveningEnd { get; set; }
    }
}