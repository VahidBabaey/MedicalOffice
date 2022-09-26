using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// جزئیات ضریب کا
/// </summary>
public class KMultiplierDetail : BaseDomainEntity<Guid>
{
    /// <summary>
    /// ضریب کا 
    /// </summary>
    public KMultiplier? KMultiplier { get; set; }
    /// <summary>
    /// آیدی ضریب کا
    /// </summary>
    public Guid KMultiplierId { get; set; }
    /// <summary>
    /// عنوان ضریب کا : فنی - حرفه ای - غیره
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// مقدار ضریب کا
    /// </summary>
    public float KValue { get; set; }
}