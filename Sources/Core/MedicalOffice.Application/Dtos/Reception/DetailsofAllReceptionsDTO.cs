using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;


namespace MedicalOffice.Application.Dtos.Reception;

public class DetailsofAllReceptionsDTO
{

    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid ReceptionId { get; set; }
    /// <summary>
    /// هزینه دریافتی
    /// </summary>
    public long Cost { get; set; }
    /// <summary>
    /// مبلغ امانی / بیعانه
    /// </summary>
    public float Deposit { get; set; }
    /// <summary>
    /// بدهی
    /// </summary>
    public float Debt { get; set; }
    /// <summary>
    /// جمع کل
    /// </summary>
    //public float Total { get; } //readonly
}