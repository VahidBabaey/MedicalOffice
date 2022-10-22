using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// پذیرش
/// </summary>
public class Reception : BaseDomainEntity<Guid>
{
    /// <summary>
    /// تایپ پذیرش : پرداخت مبلغ امانی - پرداخت بدهی - برگشتی - پکیج - بدون فرانشیز - عادی
    /// </summary>
    public ReceptionType? ReceptionType { get; set; }
    /// <summary>
    /// تاریخ پذیرش
    /// </summary>
    public string ReceptionDate { get; set; } = string.Empty;
    /// <summary>
    /// ساعت ثبت پذیرش
    /// </summary>
    public string ReceptionSubmitHour { get; set; } = string.Empty;
    /// <summary>
    /// مطب
    /// </summary>
    public Office? Office { get; set; }
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// بیمار
    /// </summary>
    public Patient? Patient { get; set; }
    /// <summary>
    /// آیدی بیمار
    /// </summary>
    public Guid PatientId { get; set; }
    /// <summary>
    /// شیفت
    /// </summary>
    public Shift? Shift { get; set; }
    /// <summary>
    /// آیدی شیفت
    /// </summary>
    public Guid ShiftId { get; set; }
    /// <summary>
    /// یوزر لاگین شده
    /// </summary>
    public MedicalStaff? MedicalStaff { get; set; }
    /// <summary>
    /// آیدی یوزر لاگین شده
    /// </summary>
    public Guid LoggedInUserId { get; set; }
    /// <summary>
    /// شماره فاکتور
    /// </summary>
    public int FactorNo { get; set; }
    /// <summary>
    /// شماره فاکتور روز
    /// </summary>
    public int FactorNoToday { get; set; }
    /// <summary>
    /// جزئیات پذیرش
    /// </summary>
    public ICollection<ReceptionDetail> ReceptionDetails { get; set; } = new List<ReceptionDetail>();
    /// <summary>
    /// جمع کلی هزینه
    /// </summary>
    public decimal TotalReceptionCost { get; set; }
    /// <summary>
    /// جمع کلی دریافتی
    /// </summary>
    public decimal TotalReceived { get; set; }
    /// <summary>
    /// جمع کلی بدهی
    /// </summary>
    public decimal TotalDebt { get; set; }
    /// <summary>
    /// جمع کلی مبلغ امانی
    /// </summary>
    public decimal TotalDeposit { get; set; }
    /// <summary>
    /// عدم مراجعه
    /// </summary>
    public bool IsCancelled { get; set; }
    /// <summary>
    /// پذیرش به طور کامل برگشت خورده یا خیر
    /// </summary>
    public bool IsReturned { get; set; }
}
