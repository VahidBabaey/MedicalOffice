using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugIntractionD
{
    public class DrugIntractionListDTO : BaseDto<Guid>
    {
        /// <summary>
        /// نام گروه1
        /// </summary>
        public string Group1 { get; set; } = string.Empty;
        /// <summary>
        /// نام گروه2
        /// </summary>
        public string Group2 { get; set; } = string.Empty;
        /// <summary>
        /// نام اثرات
        /// </summary>
        public string Effects { get; set; } = string.Empty;
        /// <summary>
        /// نام مکانیسم
        /// </summary>
        public string Method { get; set; } = string.Empty;
        /// <summary>
        /// نام روش کنترل
        /// </summary>
        public string Control { get; set; } = string.Empty;
        /// <summary>
        /// آیدی داروی اول
        /// </summary>
        public Guid? PDrugId { get; set; }
        /// <summary>
        /// نام داروی اول
        /// </summary>
        public string PDrugName { get; set; } = string.Empty;
        /// <summary>
        /// آیدی داروی دومم
        /// </summary>
        public Guid? SDrugId { get; set; }
        /// <summary>
        /// نام داروی دوم
        /// </summary>
        public string SDrugName { get; set; } = string.Empty;
    }
}
