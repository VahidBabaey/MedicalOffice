using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// تخصص
/// </summary>
public class Specialization : BaseDomainEntity<Guid>
{
    /// <summary>
    /// عنوان تخصص
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// پزشکان
    /// </summary>
    public ICollection<MedicalStaff>? Doctors { get; set; }
}