using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.OfficeDTO;

public class OfficeDTO : BaseDto<Guid>
{
    /// <summary>
    /// نام
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// آدرس
    /// </summary>
    public string Address { get; set; } = string.Empty;
    /// <summary>
    /// شماره ثابت
    /// </summary>
    public string Tel { get; set; } = string.Empty;
}
