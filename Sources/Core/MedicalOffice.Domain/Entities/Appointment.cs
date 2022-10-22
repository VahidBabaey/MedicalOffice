using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// وقت دهی
/// </summary>
public class Appointment : BaseDomainEntity<Guid>
{
    /// <summary>
    /// نوع وقت دهی
    /// </summary>
    public AppointmentType? AppointmentType { get; set; }
    /// <summary>
    /// آیدی نوع وقت دهی
    /// </summary>
    public Guid AppointmentTypeId { get; set; }
    /// <summary>
    /// پزشک
    /// </summary>
    public MedicalStaff? MedicalStaff { get; set; }
    /// <summary>
    /// آیدی پزشک
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// بیمار
    /// </summary>
    public Patient? Patient { get; set; }
    /// <summary>
    /// آیدی بیمارF
    /// </summary>
    public Guid PatientId { get; set; }
    /// <summary>
    /// نام و نام خانوادگی بیمار
    /// </summary>
    public string FullName { get; set; } = string.Empty;
    /// <summary>
    /// شماره همراه
    /// </summary>
    public string Mobile { get; set; } = string.Empty;
    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalId { get; set; } = string.Empty;
    /// <summary>
    /// خدمات وقت دهی
    /// </summary>
    public ICollection<AppointmentService>? AppointmentServices { get; set; }
    /// <summary>
    /// بیمه
    /// </summary>
    public Insurance? Insurance { get; set; }
    /// <summary>
    /// آیدی بیمه
    /// </summary>
    public Guid InsId { get; set; }
    /// <summary>
    /// کد رفرنس
    /// </summary>
    public string ReferenceCode { get; set; } = string.Empty;
    /// <summary>
    /// تاربخ وفت دهی
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
}
