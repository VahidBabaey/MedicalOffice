using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

public class Diagnose : BaseDomainEntity<Guid>
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
    /// وضعیت تشخیص
    /// </summary>
    public DiagnoseStatus DiagnoseStatus { get; set; }
    /// <summary>
    /// تاریخ تشخیص
    /// </summary>
    public string DiagnoseDate { get; set; } = string.Empty;
    /// <summary>
    /// ساعت تشخیص
    /// </summary>
    public string DiagnoseHour { get; set; } = string.Empty;
    /// <summary>
    /// شدت
    /// </summary>
    public Severity Severity { get; set; }
    /// <summary>
    /// توضیحات
    /// </summary>
    public string Description { get; set; } = string.Empty;

}
