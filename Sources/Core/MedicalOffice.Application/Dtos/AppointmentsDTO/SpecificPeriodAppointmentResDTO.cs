namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class SpecificPeriodAppointmentResDTO
    {
        public DateTime Date { get; set; }

        public int AllTimes { get; set; }

        public int FullTimes { get; set; }

        public List<FreeTime> FreeTimes { get; set; }

        public bool Full { get; set; } = false;
    }

    public class FreeTime
    {
        public FreeTime(TimeOnly startTime, TimeOnly endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
