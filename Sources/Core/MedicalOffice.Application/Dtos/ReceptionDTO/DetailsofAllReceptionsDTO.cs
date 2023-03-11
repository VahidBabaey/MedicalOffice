using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;


namespace MedicalOffice.Application.Dtos.ReceptionDTO;

public class DetailsofAllReceptionsDTO
{

    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid? ReceptionId { get; set; }
    /// <summary>
    /// هزینه دریافتی
    /// </summary>
    public long Cost { get; set; }
    /// <summary>
    /// مبلغ امانی / بیعانه
    /// </summary>
    public decimal Deposit { get; set; }
    /// <summary>
    /// بدهی
    /// </summary>
    public decimal Debt { get; set; }
    /// <summary>
    /// دریافتی
    /// </summary>
    public decimal Recieved { get; set; }
    /// <summary>
    /// شماره فاکتور
    /// </summary>
    public int FactorNo { get; set; }
    /// <summary>
    /// توضیحات
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// تاریخ پذیزش
    /// </summary>
    public string Date { get; set; }
    /// <summary>
    /// نام و نام خانوادگی یوزر ثبت کننده
    /// </summary>
    public string Username { get; set; }
}