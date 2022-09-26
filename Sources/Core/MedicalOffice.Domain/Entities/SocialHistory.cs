using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

public class SocialHistory : BaseDomainEntity<Guid>
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
    /// آی سی دی یازده
    /// </summary>
    public ICD11? ICD11 { get; set; }
    /// <summary>
    /// آیدی آی سی دی یازده
    /// </summary>
    public Guid ICD11Id { get; set; }
    /// <summary>
    /// نسبت
    /// </summary>
    public Relativity Relativity { get; set; }
    /// <summary>
    /// منجر به مرگ شده یا خیر
    /// </summary>
    public bool HasLeadToDeath { get; set; }
    /// <summary>
    /// توضیحات
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
