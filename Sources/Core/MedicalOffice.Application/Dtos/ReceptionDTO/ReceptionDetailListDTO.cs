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
    public long Recieved { get; set; }
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
    /// تعرفه
    /// </summary>
    public long Cost { get; set; }
    /// <summary>
    /// سهم سازمان
    /// </summary>
    public long OrganShare { get; set; }


}