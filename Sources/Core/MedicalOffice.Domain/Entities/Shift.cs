using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// شیفت
/// </summary>
public class Shift : BaseDomainEntity<Guid>
{
    /// <summary>
    /// مطب
    /// </summary>
    public Office? Office { get; set; }
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// عنوان شیفت : صبح - ظهر - عصر - غیره
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// تایم شروع
    /// </summary>
    public string StartTime { get; set; } = string.Empty;
    /// <summary>
    /// تایم پایان
    /// </summary>
    public string EndTime { get; set; } = string.Empty;
    /// <summary>
    /// آیا شیفت روز های تعطبل نیز هست یا خیر
    /// </summary>
    public bool HolidayShift { get; set; }
    /// <summary>
    /// پذیرش ها
    /// </summary>
    public ICollection<Reception>? Receptions { get; set; }
    /// <summary>
    /// درصد سهم کاربران
    /// </summary>
    public ICollection<MedicalStaffServiceSharePercent>? MedicalStaffServiceSharePercents { get; set; }
}