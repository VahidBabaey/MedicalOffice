using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.Patient;

public class PatientListDto : BaseDto<Guid>
{
    /// <summary>
    /// شماره پرونده
    /// </summary>
    public string FileNumber { get; set; } = string.Empty;
    /// <summary>
    /// نام و نام خانوادگی
    /// </summary>
    public string FullName { get; set; } = string.Empty;
    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalID { get; set; } = string.Empty;
    /// <summary>
    /// شماره تلفن همراه
    /// </summary>
    public string Mobile { get; set; } = string.Empty;
    /// <summary>
    /// نام پدر
    /// </summary>
    public string FatherName { get; set; } = string.Empty;
    /// <summary>
    /// تاریخ تولد
    /// </summary>
    public string BirthDate { get; set; } = string.Empty;
    /// <summary>
    /// آیدی بیمه
    /// </summary>
    public Guid InsuranceId { get; set; }

}