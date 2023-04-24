using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;


namespace MedicalOffice.Application.Dtos.ReceptionDTO;

public class DetailsOfAllReceptionsDTO
{

    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid? ReceptionId { get; set; }
    /// <summary>
    /// قابل پرداخت
    /// </summary>
    public long Payable { get; set; }
    /// <summary>
    /// جمع کل
    /// </summary>
    public long Total { get; set; }
    /// <summary>
    /// مبلغ امانی / بیعانه
    /// </summary>
    //public long Deposit { get; set; }
    /// <summary>
    /// بدهی
    /// </summary>
    public long Debt { get; set; }
    /// <summary>
    /// دریافتی
    /// </summary>
    public long Recieved { get; set; }
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