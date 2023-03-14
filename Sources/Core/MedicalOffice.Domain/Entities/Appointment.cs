using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// وقت دهی
/// </summary>
public class Appointment : BaseDomainEntity<Guid>
{
    /// <summary>
    /// شناسه مطب
    /// </summary>
    public Guid OfficeId { get; set; }

    /// <summary>
    /// ارتباط یک به چند مطب و وقت
    /// </summary>
    public Office Office { get; set; }

    /// <summary>
    /// آیدی پزشک
    /// </summary>
    public Guid MedicalStaffId { get; set; }

    /// <summary>
    /// پزشک
    /// </summary>
    public MedicalStaff MedicalStaff { get; set; }

    /// <summary>
    /// آیدی سرویس
    /// </summary>
    public Guid ServiceId { get; set; }

    /// <summary>
    /// سرویس
    /// </summary>
    public Service Service { get; set; }

    /// <summary>
    /// آیدی اتاق
    /// </summary>
    public Guid? ServiceRoomId { get; set; }

    /// <summary>
    /// اتاق
    /// </summary>
    public Room? Room{ get; set; }

    /// <summary>
    /// شناسه  دستگاه
    /// </summary>
    public Guid? DeviceId { get; set; }

    /// <summary>
    /// دستگاه
    /// </summary>
    public Device? Device { get; set; }

    /// <summary>
    ///شناسه معرف
    /// </summary>
    public Guid? ReferrerId { get; set; }

    /// <summary>
    /// همه یوزرها
    /// </summary>
    public Referrer Referrer{ get; set; }

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
    public string NationalId { get; set; } = string.Empty;
    
    /// <summary>
    /// تاریخ وفت دهی
    /// </summary>
    public DateTime Date { get; set; } = default;
    
    /// <summary>
    /// تایم شروع
    /// </summary>
    public string? StartTime { get; set; } = string.Empty;
    
    /// <summary>
    /// تایم پایان
    /// </summary>
    public string? EndTime { get; set; } = string.Empty;

    /// <summary>
    /// توضیحات وقت
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// نوع وقت دهی
    /// </summary>
    public AppointmentType AppointmentType { get; set; }

    /// <summary>
    /// خدمات وقت دهی
    /// </summary>
    public ICollection<AppointmentService>? AppointmentServices { get; set; }

    public User CreatedBy { get; set; }

    public User LastUpdatedBy { get; set; }

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
