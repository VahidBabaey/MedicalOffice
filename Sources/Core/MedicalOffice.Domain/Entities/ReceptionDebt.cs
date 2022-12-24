using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// جزئیات بدهی
/// </summary>
public class ReceptionDebt : BaseDomainEntity<Guid>
{

    /// <summary>
    /// پذیرش
    /// </summary>
    public Reception? Reception { get; set; }
    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid? ReceptionId { get; set; }
    /// <summary>
    /// مطب
    /// </summary>
    public Office? Office { get; set; }
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// وضعیت
    /// </summary>
    public int ReceptionDebtStatus { get; set; }
    /// <summary>
    /// بدهی دریافتی
    /// </summary>
    public long ReceptionDebtPrice { get; set; }
    /// <summary>
    /// اطلاعات تخفیف ها
    /// </summary>
    public ReceptionDetail? ReceptionDetail { get; set; }
    /// <summary>
    /// آیدی تخفیف
    /// </summary>
    public Guid ReceptionDetailId { get; set; }

}