using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;


namespace MedicalOffice.Application.Dtos.Reception;

public class ReceptionDTO : BaseDto<Guid>
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
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// آیدی بیمار
    /// </summary>
    public Guid PatientId { get; set; }
    /// <summary>
    /// آیدی شیفت
    /// </summary>
    public Guid ShiftId { get; set; }
    /// <summary>
    /// آیدی یوزر لاگین شده
    /// </summary>
    public Guid LoggedInMedicalStaffId { get; set; }
    /// <summary>
    /// شماره فاکتور
    /// </summary>
    public uint FactorNo { get; set; }
    /// <summary>
    /// شماره فاکتور روز
    /// </summary>
    public uint FactorNoToday { get; set; }
    /// <summary>
    /// جمع کلی هزینه
    /// </summary>
    public float TotalReceptionCost { get; set; }
    /// <summary>
    /// جمع کلی دریافتی
    /// </summary>
    public float TotalReceived { get; set; }
    /// <summary>
    /// جمع کلی بدهی
    /// </summary>
    public float TotalDebt { get; set; }
    /// <summary>
    /// جمع کلی مبلغ امانی
    /// </summary>
    public float TotalDeposit { get; set; }
    /// <summary>
    /// عدم مراجعه
    /// </summary>
    public bool IsCancelled { get; set; }
    /// <summary>
    /// پذیرش به طور کامل برگشت خورده یا خیر
    /// </summary>
    public bool IsReturned { get; set; }

}