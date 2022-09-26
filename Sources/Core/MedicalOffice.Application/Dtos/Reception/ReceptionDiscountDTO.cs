using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Discount;


namespace MedicalOffice.Application.Dtos.Reception;

public class ReceptionDiscountDTO : BaseDto<Guid>
{
    /// <summary>
    /// نوع تخفیف
    /// </summary>
    public DiscountTypeDTO? DiscountType { get; set; }
    /// <summary>
    /// آیدی نوع تخفیف
    /// </summary>
    public Guid DiscountTypeId { get; set; }
    /// <summary>
    /// مبلغ تخفیف
    /// </summary>
    public float Amount { get; set; }
    /// <summary>
    /// جزئیات پذیرش
    /// </summary>
    public ReceptionDetailDTO? ReceptionDetail { get; set; }
    /// <summary>
    /// آیدی جزئیات پذیرش
    /// </summary>
    public Guid ReceptionDetailId { get; set; }
}