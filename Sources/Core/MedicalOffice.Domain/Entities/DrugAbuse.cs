using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

public class DrugAbuse : BaseDomainEntity<Guid>
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
    /// اسنومد
    /// </summary>
    public SNOMED? SNOMED { get; set; }
    /// <summary>
    /// آیدی اسنومد
    /// </summary>
    public Guid SNOMEDId { get; set; }
    /// <summary>
    /// مقدار مصرفی
    /// </summary>
    public float ConsumptionAmount { get; set; }
    /// <summary>
    /// واحد
    /// </summary>
    public MeasuringUnit Unit { get; set; }
    /// <summary>
    /// تواتر
    /// </summary>
    public ConsumptionFrequency Frequency { get; set; }
    /// <summary>
    /// تاریخ شروع مصرف
    /// </summary>
    public string BeginDate { get; set; } = string.Empty;
    /// <summary>
    /// تاریخ پایان مصرف
    /// </summary>
    public string EndDate { get; set; } = string.Empty;
}
