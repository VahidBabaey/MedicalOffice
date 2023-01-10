using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class CheckTimeRequestDTO
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Guid? MedicalStaffId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid? DeviceId { get; set; }
        public Guid? RoomId { get; set; }
        public DateTime Date { get; set; }
    }
}
