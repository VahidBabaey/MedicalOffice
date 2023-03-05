using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// بخش
/// </summary>
public class Section : BaseDomainEntity<Guid>
{
    /// <summary>
    /// نام بخش
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// فعال یا غیرفعال
    /// </summary>
    public bool isActive { get; set; }
    
    /// <summary>
    /// خدمات زیرمجموعه
    /// </summary>
    public ICollection<Service>? Services { get; set; }   
    /// <summary>
    /// درصد سهم پزشکان
    /// </summary>
    public ICollection<MedicalStaffServiceSharePercent>? MedicalStaffServiceSharePercents { get; set; }
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    
    /// <summary>
    /// مطب
    /// </summary>
    public Office? Office { get; set; }
}