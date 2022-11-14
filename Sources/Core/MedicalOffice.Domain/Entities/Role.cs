using Microsoft.AspNetCore.Identity;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// نقش
/// </summary>
public class Role : IdentityRole<Guid> // BaseDomainEntity<Guid>
{
    public Role()
    {
        UserOfficeRoles = new List<UserOfficeRole>();
    }

    public string PersianName { get; set; } = string.Empty;

    /// <summary>
    /// نمایش در پذیرش
    /// </summary>
    public bool ShowinReception { get; set; }

    /// <summary>
    /// کادر درمان - نقش
    /// </summary>
    public ICollection<MedicalStaffRole>? MedicalStaffRoles { get; set; }

    //public string Name { get; set; } = string.Empty;

    /// <summary>
    /// کاربر - آفیس - نقش
    /// </summary>
    public ICollection<UserOfficeRole> UserOfficeRoles { get; set; }

    /// <summary>
    /// برای ارتباط چند به چند دسته دسترسی ها با نقش
    /// </summary>
    public ICollection<PermissionCategory>? Permission{ get; set; }
}
