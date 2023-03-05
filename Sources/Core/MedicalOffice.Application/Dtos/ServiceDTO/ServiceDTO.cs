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
    public string? GenericCode { get; set; }

    /// <summary>
    /// نحوه محاسبه قیمت
    /// </summary>
    public CalculationMethod CalculationMethod { get; set; }

    /// <summary>
    /// عملی هست یا نه
    /// </summary>
    public bool IsPractical { get; set; }

    /// <summary>
    /// هزینه مواد مصرفی
    /// </summary>
    public bool IsConsumingMaterials { get; set; }
}
