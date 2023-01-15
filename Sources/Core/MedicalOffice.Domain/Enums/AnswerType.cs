using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Enums
{
    /// <summary>
    /// نوع جواب آزمایش
    /// </summary>
     public enum AnswerType
    {
        /// <summary>
        /// حضوری
        /// </summary>
        InPerson = 1,
        /// <summary>
        /// غیر حضوری
        /// </summary>
        Absenteely = 2,
    }
}
