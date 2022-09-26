using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

public class MedicalAction : BaseDomainEntity<Guid>
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
    /// آر وی یو 3
    /// </summary>
    public RVU3? RVU3 { get; set; }
    /// <summary>
    /// آیدی آر وی یو 3
    /// </summary>
    public Guid RVU3Id { get; set; }
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
    /// ساعت پایان
    /// </summary>
    public string EndHour { get; set; } = string.Empty;
    /// <summary>
    /// توضیحات
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
