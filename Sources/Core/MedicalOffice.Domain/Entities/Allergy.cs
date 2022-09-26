using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

public class Allergy : BaseDomainEntity<Guid>
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
    /// اف دی او
    /// </summary>
    public FDO? FDO { get; set; }
    /// <summary>
    /// آیدی اف دی او
    /// </summary>
    public Guid FDOId { get; set; }
    /// <summary>
    /// آی سی دی یازده
    /// </summary>
    public ICD11? ICD11 { get; set; }
    /// <summary>
    /// آیدی آی سی دی یازده
    /// </summary>
    public Guid ICD11Id { get; set; }
    /// <summary>
    /// شدت
    /// </summary>
    public Severity Severity { get; set; }
    /// <summary>
    /// توضیحات
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
