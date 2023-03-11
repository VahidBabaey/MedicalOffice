using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// ضریب کا
/// </summary>
public class KMultiplier : BaseDomainEntity<Guid>
{
    /// <summary>
    /// مطب
    /// </summary>
    public Office? Office { get; set; }
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// عنوان یا سال ضریب کا
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// جزئیات ضرایب کا - فنی - حرفه ای - بیهوشی
    /// </summary>
    public ICollection<KMultiplierDetail>? KMultiplierDetails { get; set; }
}