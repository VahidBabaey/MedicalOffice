using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// اطلاعات تماس بیمار
/// </summary>
public class PatientContact : BaseDomainEntity<Guid>
{
    /// <summary>
    /// بیمار
    /// </summary>
    public Patient? Patient { get; set; }
    /// <summary>
    /// آیدی بیمار
    /// </summary>
    public Guid PatientId { get; set; }
    /// <summary>
    /// نوع اطلاعات تماس : ایمیل - شماره همراه - شماره ثابت - فکس
    /// </summary>
    public ContactType? ContactType { get; set; }
    /// <summary>
    /// اطلاعات تماس
    /// </summary>
    public string ContactValue { get; set; } = string.Empty;
    /// <summary>
    /// آیا پیشفرض است یا خیر
    /// </summary>
    public bool IsDefault { get; set; }
}
