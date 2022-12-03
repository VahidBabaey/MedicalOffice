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
        public TimeOnly MorningStart { get; set; }
        
        /// <summary>
        /// ساعت پایان صبح
        /// </summary>
        public TimeOnly MorningEnd { get; set; }
        
        /// <summary>
        /// ساعت شروع عصر
        /// </summary>
        public TimeOnly EveningStart { get; set; }
        
        /// <summary>
        /// ساعت پایان عصر
        /// </summary>
        public TimeOnly EveningEnd { get; set; }

    }
}
