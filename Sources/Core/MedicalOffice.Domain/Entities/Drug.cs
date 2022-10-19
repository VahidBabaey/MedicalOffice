using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

public class Drug : BaseDomainEntity<Guid>
{

    /// <summary>
    /// نام دارو
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// کد ژنریک
    /// </summary>
    public string GenericCode { get; set; } = string.Empty;
    /// <summary>
    /// نام برند
    /// </summary>
    public string BrandName { get; set; } = string.Empty;
    /// <summary>
    ///  بخش دارویی
    /// </summary>
    public DrugSection? DrugSection { get; set; }
    /// <summary>
    /// آیدی بخش دارویی
    /// </summary>
    public Guid? DrugSectionId { get; set; }
    /// <summary>
    ///  شکل دارویی
    /// </summary>
    public DrugShape? DrugShape { get; set; }
    /// <summary>
    /// آیدی شکل دارویی
    /// </summary>
    public Guid? DrugShapeId { get; set; }
    /// <summary>
    /// کاربرد دارویی
    /// </summary>
    public DrugUsage? DrugUsage { get; set; }
    /// <summary>
    /// آیدی کاربرد دارویی
    /// </summary>
    public Guid? DrugUsageId { get; set; }
    /// <summary>
    /// کاربرد دارویی
    /// </summary>
    public DrugConsumption? DrugConsumption { get; set; }
    /// <summary>
    /// آیدی کاربرد دارویی
    /// </summary>
    public Guid? DrugConsumptionId { get; set; }
    /// <summary>
    /// میزان مصرف
    /// </summary>
    public string Consumption { get; set; } = string.Empty;
    /// <summary>
    /// تعداد
    /// </summary>
    public float? Number { get; set; }
    /// <summary>
    /// عدم نمایش
    /// </summary>
    public bool? IsShow { get; set; }
    /// <summary>
    /// داروی ترکیبی
    /// </summary>
    public bool? IsHybrid { get; set; }
    /// <summary>
    /// تداخل داروها
    /// </summary>
    public ICollection<DrugIntraction>? PDrugs { get; set; }
    public ICollection<DrugIntraction>? SDrugs { get; set; }


}
