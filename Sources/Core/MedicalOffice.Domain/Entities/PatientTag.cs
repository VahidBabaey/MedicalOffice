using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// تگ بیماران
/// </summary>
public class PatientTag : BaseDomainEntity<Guid>
{    /// <summary>
     /// مطب
     /// </summary>
    public Office? Office { get; set; }
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// بیمار
    /// </summary>
    public Patient? Patient { get; set; }
    /// <summary>
    /// آیدی بیمار
    /// </summary>
    public Guid PatientId { get; set; }
    /// <summary>
    /// تگ
    /// </summary>
    public string Tag { get; set; } = string.Empty;
}
