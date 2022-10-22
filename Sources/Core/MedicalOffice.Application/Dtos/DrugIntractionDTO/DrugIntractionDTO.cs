using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugIntractionDTO
{
    public class DrugIntractionDTO
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
        /// آیدی داروها
        /// </summary>
        public Guid? PDrugID { get; set; }
        public Guid? SDrugID { get; set; }
    }
}
