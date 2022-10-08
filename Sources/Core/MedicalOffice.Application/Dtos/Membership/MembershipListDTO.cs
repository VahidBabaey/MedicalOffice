using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.Membership;

public class MembershipListDTO : BaseDto<Guid>
{
    /// <summary>
    /// نام بخش
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// نام سرویس ها
    /// </summary>
    public string NameServices { get; set; } = string.Empty;
    /// <summary>
    /// فعال یا غیرفعال
    /// </summary>
    public bool IsActive { get; set; }
    /// <summary>
    /// آیدی سرویس ها
    /// </summary>
    public Guid[]? ServicesId   { get; set; }
}
