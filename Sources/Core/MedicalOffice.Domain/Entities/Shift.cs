using MedicalOffice.Application.Features.AppointmentFeature.Handlers.Queries;
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
    public string StartTime { get; set; }
    
    /// <summary>
    /// تایم پایان
    /// </summary>
    public string EndTime { get; set; }
    
    /// <summary>
    /// آیا شیفت روز بعد هست؟
    /// </summary>
    public bool Nextday { get; set; }
    
    /// <summary>
    /// پذیرش ها
    /// </summary>
    public ICollection<Reception>? Receptions { get; set; }
    
    /// <summary>
    /// درصد سهم کاربران
    /// </summary>
    public ICollection<MedicalStaffServiceSharePercent>? MedicalStaffServiceSharePercents { get; set; }
}