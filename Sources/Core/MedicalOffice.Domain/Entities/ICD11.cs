using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

public class ICD11 : BaseDomainEntity<Guid>
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
    /// حساسیت ها
    /// </summary>
    public ICollection<Allergy>? Allergies { get; set; }
    /// <summary>
    /// تشخیص ها
    /// </summary>
    public ICollection<Diagnose>? Diagnoses { get; set; }
    /// <summary>
    /// معاینات کلی
    /// </summary>
    public ICollection<GeneralExamination>? GeneralExaminations { get; set; }
    /// <summary>
    /// پی ام اچ ها
    /// </summary>
    public ICollection<PMH>? PMHs { get; set; }
    /// <summary>
    /// سابقه اجتماعی
    /// </summary>
    public ICollection<SocialHistory>? SocialHistories { get; set; }
}
