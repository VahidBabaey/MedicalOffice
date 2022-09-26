namespace MedicalOffice.Application.Dtos.User;

public class UserListDTO
{
    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    /// <summary>
    /// شماره همراه
    /// </summary>
    public string MobilePhone { get; set; } = string.Empty;
    /// <summary>
    /// تخصص
    /// </summary>
    public List<int>? SpecializationsIds { get; set; }
    public List<string>? SpecializationsNames { get; set; }
}
