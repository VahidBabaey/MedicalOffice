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
    /// نام
    /// </summary>
    //public string Name { get; set; } = string.Empty;

    /// <summary>
    /// از این مدل برای برقراری ارتباط یک به چند بین نقش و کاربر-مطب-نقش استفاده می شود
    /// </summary>
    public ICollection<UserOfficeRole> UserOfficeRoles { get; set; }

    /// <summary>
    /// برای ارتباط چند به چند دسته دسترسی ها با نقش
    /// </summary>
    public ICollection<PermissionCategory>? Permission{ get; set; }
}
