using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

public class FDO : BaseDomainEntity<Guid>
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
    /// تجویز دارو ها
    /// </summary>
    public ICollection<DrugPrescription>? DrugPrescriptions { get; set; }
    /// <summary>
    /// دارو های روتین
    /// </summary>
    public ICollection<RoutineMedication>? RoutineMedications { get; set; }
    /// <summary>
    /// حساسیت ها
    /// </summary>
    public ICollection<Allergy>? Allergies { get; set; }
}
