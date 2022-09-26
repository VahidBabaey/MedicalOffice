using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Reception;


namespace MedicalOffice.Application.Dtos.Discount;

public class DiscountTypeDTO : BaseDto<Guid>
{
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// عنوان نوع تخفیف
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// تخفیف پذیرش ها
    /// </summary>
    public ICollection<ReceptionDiscountDTO>? ReceptionDiscounts { get; set; }

}