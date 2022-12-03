using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;


namespace MedicalOffice.Domain.Entities
{
    public class MedicalStaffWorkHourProgram : BaseDomainEntity<Guid>
    {
        /// <summary>
        /// کادر درمان
        /// </summary>
        public MedicalStaff? MedicalStaff { get; set; }
        
        /// <summary>
        /// آیدی کادر درمان
        /// </summary>
        public Guid MedicalStaffId { get; set; }
        
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
        public TimeSpan MorningStart { get; set; }
        
        /// <summary>
        /// ساعت پایان صبح
        /// </summary>
        public TimeSpan MorningEnd { get; set; }
        
        /// <summary>
        /// ساعت شروع عصر
        /// </summary>
        public TimeSpan EveningStart { get; set; }
        
        /// <summary>
        /// ساعت پایان عصر
        /// </summary>
        public TimeSpan EveningEnd { get; set; }

    }
}
