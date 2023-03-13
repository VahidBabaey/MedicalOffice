using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;


namespace MedicalOffice.Domain.Entities
{
    public class MedicalStaffSchedule : BaseDomainEntity<Guid>
    {
        /// <summary>
        /// شناسه مطب
        /// </summary>
        public Guid OfficeId{ get; set; }

        /// <summary>
        /// مطب
        /// </summary>
        public Office Office{ get; set; }

        /// <summary>
        /// کادر درمان
        /// </summary>
        public MedicalStaff MedicalStaff { get; set; }
        
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
        public DayOfWeek WeekDay { get; set; }
        
        /// <summary>
        /// ساعت شروع صبح
        /// </summary>
        public string MorningStart { get; set; }
        
        /// <summary>
        /// ساعت پایان صبح
        /// </summary>
        public string MorningEnd { get; set; }
        
        /// <summary>
        /// ساعت شروع عصر
        /// </summary>
        public string EveningStart { get; set; }
        
        /// <summary>
        /// ساعت پایان عصر
        /// </summary>
        public string EveningEnd { get; set; }

    }
}
