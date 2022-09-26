namespace MedicalOffice.Domain.Enums;

/// <summary>
/// نوع طلاعات تماس
/// </summary>
public enum ContactType
{
    /// <summary>
    /// تلفن همراه
    /// </summary>
    Mobile = 1,
    /// <summary>
    /// شماره ثابت - منزل
    /// </summary>
    Tel,
    /// <summary>
    /// ایمیل
    /// </summary>
    Email,
    /// <summary>
    /// فکس
    /// </summary>
    Fax
}
