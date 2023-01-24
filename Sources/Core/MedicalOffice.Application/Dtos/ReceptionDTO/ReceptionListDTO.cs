using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Domain.Entities;


namespace MedicalOffice.Application.Dtos.ReceptionDTO;

public class ReceptionListDTO : BaseDto<Guid>
{
    /// <summary>
    /// تاریخ مراجعه
    /// </summary>
    public string ReceptionDate { get; set; }
    /// <summary>
    /// سرویس ها
    /// </summary>
    public string ServicesNames { get; set; }
    /// <summary>
    /// تعداد
    /// </summary>
    public int ServiceCount { get; set; }
    /// <summary>
    /// هزینه دریافتی
    /// </summary>
    public long Cost { get; set; }
    /// <summary>
    /// مبلغ امانی / بیعانه
    /// </summary>
    public float Deposit { get; set; }
    /// <summary>
    /// بدهی
    /// </summary>
    public long Debt { get; set; }
    /// <summary>
    /// جمع کل
    /// </summary>
    public float TotalReception { get; set; }
    /// <summary>
    /// لیست  پزشکان
    /// </summary>
    public string DoctorsNames { get; set; }
    /// <summary>
    /// لیست کارشناسان
    /// </summary>
    public string ExpertsNames { get; set; }
    ///// <summary>
    /////  خدمت
    ///// </summary>
    //public ICollection<ServiceListDTO>? Services { get; set; }
    ///// <summary>
    ///// اطلاعات تعداد - مبلغ دریافتی - پیش پرداخت - بدهی
    ///// </summary>
    //public ReceptionDetailDTO? ReceptionDetailDTO { get; set; }
    ///// <summary>
    ///// اطلاعات تخفیف - 
    ///// </summary>
    //public ReceptionDiscountDTO? ReceptionDiscountDTO { get; set; }
    ///// <summary>
    ///// کاربران / پزشکان
    ///// </summary>
    //public ICollection<ReceptionMedicalStaffDTO>? ReceptionMedicalStaffs { get; set; }
}