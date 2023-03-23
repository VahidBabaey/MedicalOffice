using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Commons;
using MedicalOffice.Application.Dtos.Discount;


namespace MedicalOffice.Application.Dtos.ReceptionDTO;

public class ReceptionDiscountDTO : IMembershipIdDTO
{
    /// <summary>
    /// آیدی نوع عضویت
    /// </summary>
    public Guid MembershipId { get; set; }
    /// <summary>
    /// مبلغ تخفیف
    /// </summary>
    public int Discount { get; set; }

}