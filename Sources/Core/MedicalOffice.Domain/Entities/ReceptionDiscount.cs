using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// تخفیف پذیرش
/// </summary>
public class ReceptionDiscount : BaseDomainEntity<Guid>
{
    /// <summary>
    /// نوع عضویت
    /// </summary>
    public Membership? Membership { get; set; }
    /// <summary>
    /// آیدی نوع عضویت
    /// </summary>
    public Guid? MembershipId { get; set; }
    /// <summary>
    /// مبلغ تخفیف
    /// </summary>
    public float Discount { get; set; }

}