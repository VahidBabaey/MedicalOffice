using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.MembershipDTO;

public class MembershipsNamesDTO : BaseDto<Guid>
{
    /// <summary>
    /// نام عضویت
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
