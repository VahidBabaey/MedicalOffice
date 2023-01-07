namespace MedicalOffice.WebApi.WebApi.Controllers
{
    public class DateAppointmentDto
    {
        public DateTime Date{ get; set; }

        public int AllTimes{ get; set; }

        public int FullTimes{ get; set; }

        public List<string> FreeTimes { get; set; }

        public bool Full { get; set; } = false;
    }
}