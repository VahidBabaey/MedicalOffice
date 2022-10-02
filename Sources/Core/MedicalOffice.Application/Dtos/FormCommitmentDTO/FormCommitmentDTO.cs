using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.FormCommitmentDTO
{
    public class FormCommitmentDTO
    {
        /// <summary>
        /// نام فرم تعهدنامه
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// آیدی بیمار
        /// </summary>
        public Guid PatientId { get; set; }
    }
}
