using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// پرداخت پوز
/// </summary>
public class CashMoney : BaseDomainEntity<Guid>
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
    /// صندوق
    /// </summary>
    public Cash? Cash { get; set; }
    /// <summary>
    /// آیدی صندوق
    /// </summary>
    public Guid CashId { get; set; }
    /// <summary>
    /// پرداختی
    /// </summary>
    public long Cost { get; set; }
    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid ReceptionId { get; set; }

}