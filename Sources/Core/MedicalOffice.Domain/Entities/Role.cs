using MedicalOffice.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// نقش
/// </summary>
public class Role : IdentityRole<Guid>
{
    public Role()
    {
        UserOfficeRoles = new List<UserOfficeRole>();
    }

    public string PersianName { get; set; }

    /// <summary>
    /// نمایش در پذیرش
    /// </summary>
    public bool ShowInReception { get; set; }

    /// <summary>
    /// کادر درمان - نقش
    /// </summary>
    public ICollection<MedicalStaffRole> MedicalStaffRoles { get; set; }

    /// <summary>
    /// کاربر - آفیس - نقش
    /// </summary>
    public ICollection<UserOfficeRole> UserOfficeRoles { get; set; }

    /// <summary>
    /// برای ارتباط چند به چند دسته دسترسی ها با نقش
    /// </summary>
    public ICollection<PermissionCategory> PermissionCategory{ get; set; }
}
