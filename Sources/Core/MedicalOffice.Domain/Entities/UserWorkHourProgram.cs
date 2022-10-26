﻿using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;


namespace MedicalOffice.Domain.Entities
{
    public class UserWorkHourProgram : BaseDomainEntity<Guid>
    {
        /// <summary>
        /// کادر درمان
        /// </summary>
        public User? User { get; set; }
        /// <summary>
        /// آیدی کادر درمان
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// حداکثر تعداد نوبت‌ها در روز
        /// </summary>
        public int MaxAppointmentCount { get; set; }
        /// <summary>
        /// روزهای هفته
        /// </summary>
        public WeekDay WeekDay { get; set; }
        /// <summary>
        /// ساعت شروع صبح
        /// </summary>
        public string MorningStart { get; set; } = string.Empty;
        /// <summary>
        /// ساعت پایان صبح
        /// </summary>
        public string MorningEnd { get; set; } = string.Empty;
        /// <summary>
        /// ساعت شروع عصر
        /// </summary>
        public string EveningStart { get; set; } = string.Empty;
        /// <summary>
        /// ساعت پایان عصر
        /// </summary>
        public string EveningEnd { get; set; } = string.Empty;

    }
}
