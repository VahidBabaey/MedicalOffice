using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Commons;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.ServiceDTO;

public class ServiceDTO : ISectionIdDTO
{
    /// <summary>
    /// آیدی بخش
    /// </summary>
    public Guid SectionId { get; set; }

    /// <summary>
    /// نام خدمت
    /// </summa?ry>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// کد ژنریک
    /// </summary>
    public string? GenericCode { get; set; }=string.Empty;

    /// <summary>
    /// نام ارزش نسبی
    /// </summary>
    public string? ICD10Name { get; set; }

    /// <summary>
    /// نحوه محاسبه قیمت
    /// </summary>
    public CalculationMethod CalculationMethod { get; set; }

    /// <summary>
    /// عملی هست یا نه
    /// </summary>
    public bool? IsPractical { get; set; } = false;

    /// <summary>
    /// تعیین تعرفه در زمان پذیرش
    /// </summary>
    public bool? TariffInReceptionTime { get; set; } = false;

    /// <summary>
    /// هزینه مواد مصرفی
    /// </summary>
    public bool? IsConsumingMaterials { get; set; } = false;
}