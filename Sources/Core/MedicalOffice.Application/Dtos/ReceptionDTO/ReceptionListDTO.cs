using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Domain.Entities;


namespace MedicalOffice.Application.Dtos.ReceptionDTO;

public class ReceptionListDTO : BaseDto<Guid>
{
    /// <summary>
    /// تاریخ پذیرش
    /// </summary>
    public ReceptionsDTO? ReceptionDTO { get; set; }
    /// <summary>
    ///  خدمت
    /// </summary>
    public ICollection<ServiceListDTO>? Services { get; set; }
    /// <summary>
    /// اطلاعات تعداد - مبلغ دریافتی - پیش پرداخت - بدهی
    /// </summary>
    public ReceptionDetailDTO? ReceptionDetailDTO { get; set; }
    /// <summary>
    /// اطلاعات تخفیف - 
    /// </summary>
    public ReceptionDiscountDTO? ReceptionDiscountDTO { get; set; }
    /// <summary>
    /// کاربران / پزشکان
    /// </summary>
    public ICollection<ReceptionMedicalStaffDTO>? ReceptionMedicalStaffs { get; set; }
}