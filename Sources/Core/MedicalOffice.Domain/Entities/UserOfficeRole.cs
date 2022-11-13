using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// کاربر-مطب-نقش
/// <para>از این مدل برای دانستن نقش هر کاربر در هر مطب یا مرکز درمانی استفاده می شود</para>
/// </summary>
public class UserOfficeRole : BaseDomainEntity<Guid>
{
    public User? User { get; set; }

    /// <summary>
    /// آیدی کاربر
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// مطب - مرکز درمانی
    /// </summary>
    public Office? Office { get; set; }

    /// <summary>
    /// آیدی مطب یا مرکز درمانی
    /// </summary>
    public Guid? OfficeId { get; set; }

    /// <summary>
    /// نقش
    /// </summary>
    public Role? Role { get; set; }

    /// <summary>
    /// آیدی نقش
    /// </summary>
    public Guid? RoleId { get; set; }

    /// <summary>
    /// سهم و درصد کاربران
    /// </summary>
    //public ICollection<ServiceSharePercent>? UserServiceSharePercents { get; set; }

    /// <summary>
    /// دسترسی ها
    /// </summary>
    //public ICollection<Permission>? Permissiones { get; set; }
}
