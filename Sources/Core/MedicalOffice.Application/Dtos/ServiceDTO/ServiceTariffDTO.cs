using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.ServiceDTO;

public class ServiceTariffDTO
{
    /// <summary>
    /// آیدی بیمه
    /// </summary>
    public Guid? InsuranceId { get; set; }

    /// <summary>
    /// کد بیمه
    /// </summary>
    public int? InsuranceCode { get; set; }

    /// <summary>
    /// آیدی سرویس
    /// </summary>
    public Guid? ServiceId { get; set; }

    /// <summary>
    /// نوع خدمت
    /// </summary>
    public ServiceType? ServiceType { get; set; }

    /// <summary>
    /// مبلغ تعرفه
    /// </summary>
    public float TariffValue { get; set; }

    /// <summary>
    /// تعرفه داخلی
    /// </summary>
    public float InternalTariffValue { get; set; }

    /// <summary>
    /// ما به التفاوت
    /// </summary>
    public float Difference { get; set; }

    /// <summary>
    /// درصد بیمه
    /// </summary>
    public float InsurancePercent { get; set; }
}
