using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// تخصص کاربران / پزشکان
/// </summary>
public class UserOfficeSpecialization : BaseDomainEntity<Guid>
{
    /// <summary>
    /// کاربر
    /// </summary>
    public User? User { get; set; }
    /// <summary>
    /// آیدی کاربر
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// مرکز درمانی - مطب
    /// </summary>
    public Office? Office { get; set; }
    /// <summary>
    /// آیدی مرکز درمانی - مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// تخصص
    /// </summary>
    public Specialization? Specialization { get; set; }
    /// <summary>
    /// آیدی تخصص
    /// </summary>
    public Guid SpecializationId { get; set; }
}