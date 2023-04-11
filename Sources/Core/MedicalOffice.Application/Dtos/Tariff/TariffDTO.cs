using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.Tariff;

public class TariffDTO : IServiceIdDTO, IInsuranceIdDTO
{
    /// <summary>
    /// آیدی خدمت
    /// </summary>
    public Guid ServiceId { get; set; }

    /// <summary>
    /// آیدی بیمه
    /// </summary>
    public Guid? InsuranceId { get; set; }

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
    /// نوع سرویس
    /// </summary>
    public ServiceType ServiceType { get; set; }

    /// <summary>
    /// ما به التفاوت
    /// </summary>
    public float? Difference { get; set; } = 0;

    /// <summary>
    /// درصد بیمه
    /// </summary>
    public int? InsurancePercent { get; set; } = default;
}