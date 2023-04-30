using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// پرداخت چک
/// </summary>
public class CashCheck : BaseDomainEntity<Guid>
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
    /// شماره حساب
    /// </summary>
    public string AccountNumber { get; set; }
    /// <summary>
    /// شعبه
    /// </summary>
    public string Branch { get; set; }
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
    /// <summary>
    /// نوع پرداخت
    /// </summary>
    public CashType CashType { get; set; }

}