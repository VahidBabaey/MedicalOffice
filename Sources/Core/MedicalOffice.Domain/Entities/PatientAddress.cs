using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// آدرس بیمار
/// </summary>
public class PatientAddress : BaseDomainEntity<Guid>
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
    /// نوع آدرس : خانه - محل کار - سایر
    /// </summary>
    public AddressType? AddressType { get; set; }
    /// <summary>
    /// آدرس
    /// </summary>
    public string AddressValue { get; set; } = string.Empty;
    /// <summary>
    /// آیا پیشفرض است یا خیر
    /// </summary>
    public bool IsDefault { get; set; }
}
