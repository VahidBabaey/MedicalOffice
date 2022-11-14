using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// نقش
/// </summary>
public class Role : BaseDomainEntity<Guid>
{
    /// <summary>
    /// نام
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// نمایش در پذیرش
    /// </summary>
    public bool ShowinReception { get; set; }
    /// <summary>
    /// کادر درمان - نقش
    /// </summary>
    public ICollection<MedicalStaffRole>? MedicalStaffRoles { get; set; }
    /// <summary>
    /// کاربر - آفیس - نقش
    /// </summary>
    public ICollection<UserOfficeRole>? UserOfficeRoles { get; set; }
}
