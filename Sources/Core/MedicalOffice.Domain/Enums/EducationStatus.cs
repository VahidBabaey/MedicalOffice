namespace MedicalOffice.Domain.Enums;

/// <summary>
/// وضعیت تحصیلی
/// </summary>
public enum EducationStatuses
{
    /// <summary>
    /// بی سواد
    /// </summary>
    NoEducation = 1,
    /// <summary>
    /// دانش آموز یا دانشجو
    /// </summary>
    Student,
    /// <summary>
    /// دیپلم
    /// </summary>
    Diploma,
    /// <summary>
    /// فوق دیپلم
    /// </summary>
    AssociateDegree,
    /// <summary>
    /// لیسانس
    /// </summary>
    BachelorDegree,
    /// <summary>
    /// ارشد
    /// </summary>
    MasterDegree,
    /// <summary>
    /// دکتری
    /// </summary>
    Phd
}