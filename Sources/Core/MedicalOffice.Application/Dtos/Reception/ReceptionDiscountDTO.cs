using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Discount;


namespace MedicalOffice.Application.Dtos.Reception;

public class ReceptionDiscountDTO 
{

    /// <summary>
    /// آیدی نوع تخفیف
    /// </summary>
    public Guid? DiscountTypeId { get; set; }
    /// <summary>
    /// آیدی نوع عضویت
    /// </summary>
    public Guid? MembershipId { get; set; }
    /// <summary>
    /// مبلغ تخفیف
    /// </summary>
    public float Discount { get; set; }

}