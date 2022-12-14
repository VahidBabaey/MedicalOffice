namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class DateAppointmentDTO
    {
        public DateTime Date { get; set; }

        public int AllTimes { get; set; }

        public int FullTimes { get; set; }

        public List<string> FreeTimes { get; set; }

        public bool Full { get; set; } = false;
    }
}
