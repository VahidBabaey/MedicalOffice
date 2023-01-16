using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

public class DrugPrescription : BaseDomainEntity<Guid>
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
    /// بیمار
    /// </summary>
    public Patient? Patient { get; set; }
    /// <summary>
    /// آیدی بیمار
    /// </summary>
    public Guid PatientId { get; set; }
    /// <summary>
    /// تاریخ تجویز
    /// </summary>
    public string PrescriptionDate { get; set; } = string.Empty;
    /// <summary>
    /// ساعت تجویز
    /// </summary>
    public string PrescriptionHour { get; set; } = string.Empty;
    /// <summary>
    /// اف دی او
    /// </summary>
    public FDO? FDO { get; set; }
    /// <summary>
    /// آیدی اف دی او
    /// </summary>
    public Guid FDOId { get; set; }
    /// <summary>
    /// تواتر مصرف
    /// </summary>
    public ConsumptionFrequency ConsumptionFrequency { get; set; }
    /// <summary>
    /// طریقه مصرف
    /// </summary>
    public ConsumptionWay ConsumptionWay { get; set; }
    /// <summary>
    /// تعداد
    /// </summary>
    public float ConsumedNumber { get; set; }
    /// <summary>
    /// دوز
    /// </summary>
    public float Dose { get; set; }
    /// <summary>
    /// واحد
    /// </summary>
    public MeasuringUnit Unit { get; set; }
    /// <summary>
    /// توضیحات
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// نام ژنریک
    /// </summary>
    public string GenericName { get; set; } = string.Empty;
    /// <summary>
    /// شکل دارو
    /// </summary>
    public DrugType DrugType { get; set; }
    /// <summary>
    /// مدت زمان مصرف
    /// </summary>
    public Duration Duration { get; set; }

}
