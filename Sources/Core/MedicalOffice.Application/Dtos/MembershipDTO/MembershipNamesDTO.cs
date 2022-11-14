using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.MembershipDTO;

public class MembershipNamesDTO : BaseDto<Guid>
{
    /// <summary>
    /// نام بخش
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// تخفیف
    /// </summary>
    public string Discount { get; set; } = string.Empty;
}
