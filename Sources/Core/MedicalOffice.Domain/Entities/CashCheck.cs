using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// پرداخت چک
/// </summary>
public class CashCheck : BaseDomainEntity<Guid>
{

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
    /// شماره حساب
    /// </summary>
    public string AccountNumber { get; set; }
    /// <summary>
    /// تاریخ
    /// </summary>
    public string Date { get; set; }
    /// <summary>
    /// بانک
    /// </summary>
    public Bank? Bank { get; set; }
    /// <summary>
    /// آیدی بانک
    /// </summary>
    public Guid BankId { get; set; }
    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid ReceptionId { get; set; }
}