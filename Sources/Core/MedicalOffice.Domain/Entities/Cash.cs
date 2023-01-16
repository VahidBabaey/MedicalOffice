using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// صندوق
/// </summary>
public class Cash : BaseDomainEntity<Guid>
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
    /// پذیرش
    /// </summary>
    public Reception? Reception { get; set; }
    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid ReceptionId { get; set; }
    /// <summary>
    /// دریافتی
    /// </summary>
    public long Recieved { get; set; }
    /// <summary>
    /// تاریخ ارسال به صندوق
    /// </summary>
    public DateTime SendDate { get; set; }
    /// <summary>
    /// جزئیات پرداخت
    /// </summary>
    public ICollection<CashPos> CashPoses { get; set; } = new List<CashPos>();
    public ICollection<CashCheck> CashChecks { get; set; } = new List<CashCheck>();
    public ICollection<CashCart> CashCarts { get; set; } = new List<CashCart>();

}