using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

public class RoutineMedication : BaseDomainEntity<Guid>
{
    /// <summary>
    /// بیمار
    /// </summary>
    public Patient? Patient { get; set; }
    /// <summary>
    /// آیدی بیمار
    /// </summary>
    public Guid PatientId { get; set; }
    /// <summary>
    /// اف دی او
    /// </summary>
    public FDO? FDO { get; set; }
    /// <summary>
    /// آیدی اف دی او
    /// </summary>
    public Guid FDOId { get; set; }
    /// <summary>
    /// طریقه مصرف
    /// </summary>
    public ConsumptionWay ConsumptionWay { get; set; }
}
