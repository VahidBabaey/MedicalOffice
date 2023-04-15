using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;


namespace MedicalOffice.Application.Dtos.ReceptionDTO;

public class ReceptionDetailListDTO : BaseDto<Guid>
{

    /// <summary>
    /// نام خدمت
    /// </summary>
    public string ServiceName { get; set; }
    /// <summary>
    /// آی دی سرویس
    /// </summary>
    public Guid? ServiceId { get; set; }
    /// <summary>
    /// تعداد
    /// </summary>
    public int ServiceCount { get; set; }
    /// <summary>
    /// آی دی بیمه
    /// </summary>
    public Guid? InsuranceId { get; set; }
    /// <summary>
    /// آی دی بیمه تکمیلی
    /// </summary>
    public Guid? AdditionalInsuranceId { get; set; }
    /// <summary>
    /// لیست تیم پزشکی
    /// </summary>
    public List<object> MedicalStaffs { get; set; }

    /// <summary>
    /// هزینه دریافتی
    /// </summary>
    public long Recieved { get; set; }

    /// <summary>
    /// تعرفه سرویس
    /// </summary>
    public long Tariff { get; set; }
    /// <summary>
    /// قابل پرداخت
    /// </summary>
    public long Payable { get; set; }
    /// <summary>
    /// جمع کل
    /// </summary>
    public long Total { get; set; }
    /// <summary>
    /// تخفیف
    /// </summary>
    public long Discount { get; set; }

    /// <summary>
    /// مبلغ امانی / بیعانه
    /// </summary>
    public long Deposit { get; set; }

    /// <summary>
    /// بدهی
    /// </summary>
    public long Debt { get; set; }

    /// <summary>
    /// سهم سازمان
    /// </summary>
    public long OrganShare { get; set; }

    /// <summary>
    /// سهم بیمار
    /// </summary>
    public long PatientShare { get; set; }

    /// <summary>
    /// سهم بیمه تکمیلی
    /// </summary>
    public long AdditionalInsuranceShare { get; set; }
}