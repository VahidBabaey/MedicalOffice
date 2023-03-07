using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.ServiceDTO;

public class ServiceListDTO : BaseDto<Guid>
{
    /// <summary>
    /// آیدی بخش
    /// </summary>
    public Guid? SectionId { get; set; }
    
    /// <summary>
    /// نام خدمت
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// کد ژنریک
    /// </summary>
    public string GenericCode { get; set; } = string.Empty;

    /// <summary>
    /// نحوه محاسبه
    /// </summary>
    public CalculationMethod CalculationMethod { get; set; }

    /// <summary>
    /// عملی هست یا نه
    /// </summary>
    public bool? IsPractical { get; set; }

    /// <summary>
    /// هزینه مواد مصرفی
    /// </summary>
    public bool? IsConsumingMaterials { get; set; }

    /// <summary>
    /// تعیین تعرفه در زمان پذیرش
    /// </summary>
    public bool? TariffInReceptionTime { get; set; } = false;
}
