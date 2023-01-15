﻿using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.ExperimentDTO
{
    public class ExperimentDTO
    {
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
