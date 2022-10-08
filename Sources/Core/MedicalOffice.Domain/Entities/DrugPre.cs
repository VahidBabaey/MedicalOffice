using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

public class DrugPre : BaseDomainEntity<Guid>
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

    ///// <summary>
    ///// گروه
    ///// </summary>
    //public DrugGroup? DrugGroup { get; set; }
    ///// <summary>
    ///// آیدی گروه
    ///// </summary>
    //public Guid? DrugGroupId { get; set; }
    ///// <summary>
    ///// اثرات
    ///// </summary>
    //public DrugEffects? DrugEffects { get; set; }
    ///// <summary>
    ///// آیدی اثرات
    ///// </summary>
    //public Guid? DrugEffectsId { get; set; }
    ///// <summary>
    ///// مکانیسم
    ///// </summary>
    //public DrugMethod? DrugMethod { get; set; }
    ///// <summary>
    ///// آیدی مکانیسم
    ///// </summary>
    //public Guid? DrugMethodId { get; set; }
    ///// <summary>
    ///// روش کنترل
    ///// </summary>
    //public DrugControl? DrugControl { get; set; }
    ///// <summary>
    ///// آیدی روش کنترل
    ///// </summary>
    //public Guid? DrugControlId { get; set; }
    ///// <summary>
    ///// آیدی اسنومد
    ///// </summary>
    //public Guid SNOMEDId { get; set; }
    ///// <summary>
    ///// مقدار مصرفی
    ///// </summary>
    //public float ConsumptionAmount { get; set; }
    ///// <summary>
    ///// واحد
    ///// </summary>
    //public MeasuringUnit Unit { get; set; }
    ///// <summary>
    ///// تواتر
    ///// </summary>
    //public ConsumptionFrequency Frequency { get; set; }
    ///// <summary>
    ///// تاریخ شروع مصرف
    ///// </summary>
    //public string BeginDate { get; set; } = string.Empty;
    ///// <summary>
    ///// تاریخ پایان مصرف
    ///// </summary>
    //public string EndDate { get; set; } = string.Empty;


}
