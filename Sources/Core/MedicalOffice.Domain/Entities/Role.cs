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
    /// کاربر - آفیس - نقش
    /// </summary>
    public ICollection<UserOfficeRole> UserOfficeRoles { get; set; }

    /// <summary>
    /// ارتباط چند به چند نقش و دسترسی
    /// </summary>
    public ICollection<RolePermission> RolePermissions { get; set; }
}
