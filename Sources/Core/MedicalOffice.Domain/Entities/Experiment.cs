using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class Experiment : BaseDomainEntity<Guid>
    {

        /// <summary>
        /// مطب
        /// </summary>
        public Office? Office { get; set; }
        /// <summary>
        /// آیدی مطب
        /// </summary>
        public Guid OfficeId { get; set; }
        /// <summary>
        /// نام آزمایش
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// کد ژنریک
        /// </summary>
        public string GenericCode { get; set; } = string.Empty;
        /// <summary>
        /// نوع جواب
        /// </summary>
        public AnswerType AnswerType { get; set; }
        /// <summary>
        /// ماکسیمم رنج نرمال
        /// </summary>
        public string MaxNormalRange { get; set; } = string.Empty;
        /// <summary>
        /// مینیمم رنج نرمال
        /// </summary>
        public string MinNormalRange { get; set; } = string.Empty;
        /// <summary>
        /// واحد اندازه گیری
        /// </summary>
        public string MeasuringDivision { get; set; } = string.Empty;

    }
}
