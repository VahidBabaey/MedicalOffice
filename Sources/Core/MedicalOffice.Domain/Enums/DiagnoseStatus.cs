namespace MedicalOffice.Domain.Enums;

/// <summary>
/// وضعیت تشخیص
/// </summary>
public enum DiagnoseStatus
{
    /// <summary>
    /// تشخیص اولیه
    /// </summary>
    Initial = 1,
    /// <summary>
    /// تشخیص حین درمان
    /// </summary>
    DuringTreatment = 2,
    /// <summary>
    /// تشخیص نهایی
    /// </summary>
    Final = 3
}
