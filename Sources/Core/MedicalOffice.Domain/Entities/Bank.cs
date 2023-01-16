using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// بانک
/// </summary>
public class Bank : BaseDomainEntity<Guid>
{
    /// <summary>
    /// بانک
    /// </summary>
    public string BankName { get; set; } = string.Empty;
    /// <summary>
    /// جزئیات پرداخت
    /// </summary>
    public ICollection<CashPos> CashPoses { get; set; } = new List<CashPos>();
    public ICollection<CashCheck> CashChecks { get; set; } = new List<CashCheck>();
    public ICollection<CashCart> CashCarts { get; set; } = new List<CashCart>();
}