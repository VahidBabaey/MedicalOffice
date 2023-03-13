using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.Tariff;

public class TariffListDTO : BaseDto<Guid>
{
    /// <summary>
    /// آیدی سرویس
    /// </summary>
    public Guid ServiceId { get; set; }

    /// <summary>
    /// نوع خدمت
    /// </summary>
    public ServiceType ServiceType { get; set; }

    /// <summary>
    /// آیدی بیمه
    /// </summary>
    public Guid? InsuranceId { get; set; }

    /// <summary>
    /// نام بیمه
    /// </summary>
    public string InsuranceName { get; set; } = string.Empty;

    /// <summary>
    /// کد بیمه
    /// </summary>
    public long? TariffInsuranceCode { get; set; }

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
    /// تخفیف
    /// </summary>
    public int Discount { get; set; }

    /// <summary>
    /// درصد بیمه
    /// </summary>
    public int InsurancePercent { get; set; }
}
