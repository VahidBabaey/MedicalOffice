using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.OfficeDTO;

public class OfficeListDTO : BaseDto<Guid> , ITelePhoneNumberDTO
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
    public string TelePhoneNumber { get; set; } = string.Empty;
    /// <summary>
    /// نوع تعرفه
    /// </summary>
    public TariffType TariffType { get; set; }
}
