using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugD
{
    public class UpdateDrugDTO : BaseDto<Guid>
    {
        /// <summary>
        /// نام دارو
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// کد ژنریک
        /// </summary>
        public string GenericCode { get; set; } = string.Empty;
        /// <summary>
        /// نام برند
        /// </summary>
        public string BrandName { get; set; } = string.Empty;
        /// <summary>
        /// آیدی بخش دارویی
        /// </summary>
        public Guid? DrugSectionId { get; set; }
        /// <summary>
        /// آیدی شکل دارویی
        /// </summary>
        public Guid? DrugShapeId { get; set; }
        /// <summary>
        /// آیدی کاربرد دارویی
        /// </summary>
        public Guid? DrugUsageId { get; set; }
        /// <summary>
        /// آیدی کاربرد دارویی
        /// </summary>
        public Guid? DrugConsumptionId { get; set; }
        /// <summary>
        /// میزان مصرف
        /// </summary>
        public string Consumption { get; set; } = string.Empty;
        /// <summary>
        /// تعداد
        /// </summary>
        public float? Number { get; set; }
        /// <summary>
        /// عدم نمایش
        /// </summary>
        public bool? IsShow { get; set; }
        /// <summary>
        /// داروی ترکیبی
        /// </summary>
        public bool? IsHybrid { get; set; }
    }
}
