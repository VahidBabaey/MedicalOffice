using MedicalOffice.Application.Dtos.BaseInfo;
using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.User;

public class UserDTO : BaseDto<Guid>
{
    /// <summary>
    /// نقش کاربر
    /// </summary>
    public ICollection<RoleDTO>? Roles { get; set; }
    /// <summary>
    /// کد ملی
    /// </summary>
    public string MedicalCode { get; set; } = string.Empty;
    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalID { get; set; } = string.Empty;
    /// <summary>
    /// عنوان کاربر
    /// </summary>
    public string Topic { get; set; } = string.Empty;
    /// <summary>
    /// تخصص کاربر
    /// </summary>
    public IList<SpecializationsDTO>? Specializations { get; set; }
    /// <summary>
    /// شماره همراه
    /// </summary>
    public string MobilePhone { get; set; } = string.Empty;
    /// <summary>
    /// نام کاربری
    /// </summary>
    public string Username { get; set; } = string.Empty;
    /// <summary>
    /// هش رمز ورود
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;
    /// <summary>
    /// آیدی مطب یا مرکز درمانی
    /// </summary>
    public Guid OfficeId { get; set; }
}
