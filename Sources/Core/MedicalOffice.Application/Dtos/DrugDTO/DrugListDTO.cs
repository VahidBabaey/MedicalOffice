﻿using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugDTO
{
    public class DrugListDTO : BaseDto<Guid>
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
        /// نام بخش دارویی
        /// </summary>
        public string DrugSectionName { get; set; } = string.Empty;
        /// <summary>
        /// آیدی شکل دارویی
        /// </summary>
        public Guid? DrugShapeId { get; set; }
        /// <summary>
        /// نام شکل دارویی
        /// </summary>
        public string DrugShapeName { get; set; } = string.Empty;
        /// <summary>
        /// آیدی کاربرد دارویی
        /// </summary>
        public Guid? DrugUsageId { get; set; }
        /// <summary>
        /// نام کاربرد دارویی
        /// </summary>
        public string DrugUsageName { get; set; } = string.Empty;
        /// <summary>
        /// آیدی مصرف دارویی
        /// </summary>
        public Guid? DrugConsumptionId { get; set; }
        /// <summary>
        /// نام مصرف دارویی
        /// </summary>
        public string DrugConsumptionName { get; set; } = string.Empty;
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
