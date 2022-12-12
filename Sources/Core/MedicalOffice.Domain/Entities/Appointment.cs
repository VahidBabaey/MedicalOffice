using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// وقت دهی
/// </summary>
public class Appointment : BaseDomainEntity<Guid>
{
    /// <summary>
    /// نوع وقت دهی
    /// </summary>
    public AppointmentType AppointmentType { get; set; }
    
    /// <summary>
    /// پزشک
    /// </summary>
    public MedicalStaff MedicalStaff { get; set; }
    
    /// <summary>
    /// آیدی پزشک
    /// </summary>
    public Guid MedicalStaffId { get; set; }

    /// <summary>
    /// سرویس
    /// </summary>
    public Service Service { get; set; }

    /// <summary>
    /// آیدی سرویس
    /// </summary>
    public Guid ServiceId{ get; set; }

    /// <summary>
    /// اتاق یا بخش
    /// </summary>
    public Section Section{ get; set; }

    /// <summary>
    /// آیدی اتاق یا بخش
    /// </summary>
    public Guid SectionId{ get; set; }

    /// <summary>
    /// نام بیمار
    /// </summary>
    public string PatientName { get; set; } = string.Empty;

    /// <summary>
    /// نام خانوادگی بیمار
    /// </summary>
    public string PatientLastName { get; set; } = string.Empty;

    /// <summary>
    /// شماره همراه
    /// </summary>
    public string PhoneNumber { get; set; }
    
    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalID { get; set; } = string.Empty;
    
    /// <summary>
    /// تاریخ وفت دهی
    /// </summary>
    public DateTime Date { get; set; } = default;
    
    /// <summary>
    /// تایم شروع
    /// </summary>
    public string StartTime { get; set; } = string.Empty;
    
    /// <summary>
    /// تایم پایان
    /// </summary>
    public string EndTime { get; set; } = string.Empty;
    
    /// <summary>
    /// خدمات وقت دهی
    /// </summary>
    public ICollection<AppointmentService>? AppointmentServices { get; set; }

    #region UnnecessaryProperties
    ///// <summary>
    ///// بیمار
    ///// </summary>
    //public Patient? Patient { get; set; }
    ///// <summary>
    ///// آیدی بیمارF
    ///// </summary>
    //public Guid PatientId { get; set; }
    ///// <summary>
    ///// بیمه
    ///// </summary>
    //public Insurance? Insurance { get; set; }
    ///// <summary>
    ///// آیدی بیمه
    ///// </summary>
    //public Guid InsId { get; set; }
    ///// <summary>
    ///// کد رفرنس
    ///// </summary>
    //public string ReferenceCode { get; set; } = string.Empty;
    #endregion
}
