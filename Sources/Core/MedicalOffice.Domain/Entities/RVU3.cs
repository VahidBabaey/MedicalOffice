using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

public class RVU3 : BaseDomainEntity<Guid>
{
    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalCode { get; set; } = string.Empty;
    /// <summary>
    /// نام لاتین
    /// </summary>
    public string LatinName { get; set; } = string.Empty;
    /// <summary>
    /// نام فارسی
    /// </summary>
    public string PersianName { get; set; } = string.Empty;
    /// <summary>
    /// اقدامات درمانی
    /// </summary>
    public ICollection<MedicalAction>? MedicalActions { get; set; }
}