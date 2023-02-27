using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// عضویت
/// </summary>
public class Membership : BaseDomainEntity<Guid>
{
    /// <summary>
    /// مطب
    /// </summary>
    public Office Office { get; set; }

    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }

    /// <summary>
    /// نام عضویت
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// تخفیف
    /// </summary>
    public string Discount { get; set; } = string.Empty;

    /// <summary>
    /// فعال یا غیرفال
    /// </summary>
    public bool IsActive{ get; set; }

    /// <summary>
    /// تخفیف پذیرش
    /// </summary>
    public ICollection<ReceptionDiscount>? ReceptionDiscounts { get; set; }
    
    /// <summary>
    /// از این مدل برای برقراری ارتباط یک به چند بین عضویت-سرویس استفاده می شود
    /// </summary>
    public ICollection<MemberShipService> MemberShipServices { get; set; } = new List<MemberShipService>();

}