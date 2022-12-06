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
    public MedicalStaff? MedicalStaff { get; set; }
    
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
    /// نام بیمار
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// نام خانوادگی بیمار
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// شماره همراه
    /// </summary>
    public List<string> PhoneNumber { get; set; }
    
    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalId { get; set; } = string.Empty;
    
    /// <summary>
    /// خدمات وقت دهی
    /// </summary>
    public ICollection<AppointmentService>? AppointmentServices { get; set; }
    
    /// <summary>
    /// تاریخ وفت دهی
    /// </summary>
    public string Date { get; set; } = string.Empty;
    
    /// <summary>
    /// تایم شروع
    /// </summary>
    public string StartTime { get; set; } = string.Empty;
    
    /// <summary>
    /// تایم پایان
    /// </summary>
    public string EndTime { get; set; } = string.Empty;

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
