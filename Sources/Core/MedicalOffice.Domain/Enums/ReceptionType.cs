namespace MedicalOffice.Domain.Enums;

/// <summary>
/// نوع پذیرش
/// </summary>
public enum ReceptionType
{
    /// <summary>
    /// عادی
    /// </summary>
    Normal = 1,
    /// <summary>
    /// بدون فرانشیز
    /// </summary>
    WithoutFranchise,
    /// <summary>
    /// پرداخت بدهی
    /// </summary>
    PayDebt,
    /// <summary>
    /// پرداخت مبلغ امانی
    /// </summary>
    PayDeposit,
    /// <summary>
    /// پکیج
    /// </summary>
    Package,
    /// <summary>
    /// پذیرش برگشتی
    /// </summary>
    Returned
}
