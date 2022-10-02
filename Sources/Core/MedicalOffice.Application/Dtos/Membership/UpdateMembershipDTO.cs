using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.Membership;

public class UpdateMembershipDTO : BaseDto<Guid>
{
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// نام عضویت
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// فعال یا غیرفعال
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// آیدی تخفیف پذیرش
    /// </summary>
    public Guid? ReceptionDiscountId { get; set; }
    /// <summary>
    /// آیدی لیست سرویس ها
    /// </summary>
    public Guid[]? ServiceIDs  { get; set; }


}
