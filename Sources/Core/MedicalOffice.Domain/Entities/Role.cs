using MedicalOffice.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// نقش
/// </summary>
public class Role : IdentityRole<Guid> // BaseDomainEntity<Guid>
{
    /// <summary>
    /// نام
    /// </summary>
    //public string Name { get; set; } = string.Empty;

    /// <summary>
    /// از این مدل برای برقراری ارتباط یک به چند بین نقش و کاربر-مطب-نقش استفاده می شود
    /// </summary>
    public ICollection<UserOfficeRole>? UserOfficeRoles { get; set; }
}
