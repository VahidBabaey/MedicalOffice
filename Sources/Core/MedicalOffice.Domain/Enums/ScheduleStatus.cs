using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Enums
{
    public enum ScheduleStatus
    {
        Approved = 1,
        BetweenPatients,
        Canceled,
        FreeTime,
        FinalApproval,
        waiting
    }
}
