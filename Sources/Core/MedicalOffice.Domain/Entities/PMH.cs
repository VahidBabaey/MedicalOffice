using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

public class PMH : BaseDomainEntity<Guid>
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
    /// توضیحات
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// تاریخ شروع
    /// </summary>
    public string BeginDate { get; set; } = string.Empty;
    /// <summary>
    /// تاریخ پایان
    /// </summary>
    public string EndDate { get; set; } = string.Empty;
}
