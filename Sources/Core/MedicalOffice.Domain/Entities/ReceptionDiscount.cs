using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// تخفیف پذیرش
/// </summary>
public class ReceptionDiscount : BaseDomainEntity<Guid>
{
    /// <summary>
    /// نوع تخفیف
    /// </summary>
    public DiscountType? DiscountType { get; set; }
    /// <summary>
    /// آیدی نوع تخفیف
    /// </summary>
    public Guid DiscountTypeId { get; set; }
    /// <summary>
    /// مبلغ تخفیف
    /// </summary>
    public long Amount { get; set; }
    /// <summary>
    /// جزئیات پذیرش
    /// </summary>
    public ReceptionDetail? ReceptionDetail { get; set; }
    /// <summary>
    /// آیدی جزئیات پذیرش
    /// </summary>
    public Guid? ReceptionDetailId { get; set; }
    /// <summary>
    /// عضویت ها
    /// </summary>
    public ICollection<Membership>? Memberships { get; set; }
}