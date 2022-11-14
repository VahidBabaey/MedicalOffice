using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// تخصص کاربران / پزشکان
/// </summary>
public class MedicalStaffOfficeSpecialization : BaseDomainEntity<Guid>
{
    /// <summary>
    /// کاربر
    /// </summary>
    public MedicalStaff? MedicalStaff { get; set; }
    /// <summary>
    /// آیدی کاربر
    /// </summary>
    public Guid MedicalStaffId { get; set; }
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