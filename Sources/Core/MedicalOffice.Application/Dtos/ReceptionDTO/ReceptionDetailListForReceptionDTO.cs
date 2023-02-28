using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;


namespace MedicalOffice.Application.Dtos.ReceptionDTO;

public class ReceptionDetailListForReceptionDTO : BaseDto<Guid>
{

    /// <summary>
    /// نام خدمت
    /// </summary>
    public string ServiceName { get; set; }
    /// <summary>
    /// تعداد
    /// </summary>
    public float ServiceCount { get; set; }
    /// <summary>
    /// لیست تیم پزشکی
    /// </summary>
    public string MedicalStaffs { get; set; }
    /// <summary>
    /// هزینه دریافتی
    /// </summary>
    public long Cost { get; set; }
    /// <summary>
    /// تخفیف
    /// </summary>
    public long Discount { get; set; }
    /// <summary>
    /// مبلغ امانی / بیعانه
    /// </summary>
    public float Deposit { get; set; }
    /// <summary>
    /// بدهی
    /// </summary>
    public float Debt { get; set; }
    /// <summary>
    /// جمع کل
    /// </summary>
    public float Total { get; set; }
    /// <summary>
    /// لیست  پزشکان
    /// </summary>
    public string DoctorsNames { get; set; }
    /// <summary>
    /// لیست کارشناسان
    /// </summary>
    public string ExpertsNames { get; set; }

}