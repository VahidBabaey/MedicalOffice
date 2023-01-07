using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// نوع تخفیف
/// </summary>
public class DiscountType : BaseDomainEntity<Guid>
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
    /// عنوان نوع تخفیف
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// تخفیف پذیرش ها
    /// </summary>
    public ICollection<ReceptionDiscount>? ReceptionDiscounts { get; set; }

}