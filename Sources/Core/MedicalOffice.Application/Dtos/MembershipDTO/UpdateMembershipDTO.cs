using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.MembershipDTO;

public class UpdateMembershipDTO : BaseDto<Guid>
{
    /// <summary>
    /// نام عضویت
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// فعال یا غیرفعال
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// تخفیف
    /// </summary>
    public float Discount { get; set; }


}
