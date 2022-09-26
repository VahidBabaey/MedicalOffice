using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

public class SNOMED : BaseDomainEntity<Guid>
{
    /// <summary>
    /// کد بین المللی
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
    /// سوابق دخانیات
    /// </summary>
    public ICollection<DrugAbuse>? DrugAbuses { get; set; }
}
