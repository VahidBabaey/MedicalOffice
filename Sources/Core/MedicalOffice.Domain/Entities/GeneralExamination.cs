using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

public class GeneralExamination : BaseDomainEntity<Guid>
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
    /// آیدی آ سی دی یازده
    /// </summary>
    public Guid ICD11Id { get; set; }
    /// <summary>
    /// شدت
    /// </summary>
    public Severity Severity { get; set; }
    /// <summary>
    /// تاریخ شروع
    /// </summary>
    public string BeginDate { get; set; } = string.Empty;
    /// <summary>
    /// ساعت شروع
    /// </summary>
    public string BeginHour { get; set; } = string.Empty;
    /// <summary>
    /// تاریخ پایان
    /// </summary>
    public string EndDate { get; set; } = string.Empty;
    /// <summary>
    /// وجود علائم کنونی
    /// </summary>
    public bool AnyPresentSign { get; set; }
    /// <summary>
    /// توضیحات
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
