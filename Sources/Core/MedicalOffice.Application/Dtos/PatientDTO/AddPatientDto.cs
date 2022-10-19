using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.PatientDTO;

public class PatientDTO
{
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// شماره پرونده
    /// </summary>
    public string FileNumber { get; set; } = string.Empty;
    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    /// <summary>
    /// جنسیت
    /// </summary>
    public Gender? Gender { get; set; }
    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalID { get; set; } = string.Empty;
    /// <summary>
    /// تاریخ تولد
    /// </summary>
    public string BirthDate { get; set; } = string.Empty;
    /// <summary>
    /// نام پدر
    /// </summary>
    public string FatherName { get; set; } = string.Empty;
    /// <summary>
    /// نحوه آشنایی
    /// </summary>
    public AcquaintedWay? AcquaintedWay { get; set; }
    /// <summary>
    /// وضعیت تاهل
    /// </summary>
    public MaritalStatus? MaritalStatus { get; set; }
    /// <summary>
    /// تاریخ ازدواج
    /// </summary>
    public string MarriageDate { get; set; } = string.Empty;
    /// <summary>
    /// وضعیت تحصیلی
    /// </summary>
    public EducationStatuses? EducationStatus { get; set; }
    /// <summary>
    /// شغل
    /// </summary>
    public string Occupation { get; set; } = string.Empty;
    /// <summary>
    /// آدرس
    /// </summary>
    public string[]? Address { get; set; }
    /// <summary>
    /// شماره ثابت
    /// </summary>
    public string[]? Tel { get; set; }
    /// <summary>
    /// شماره همراه
    /// </summary>
    public string[]? Mobile { get; set; }
    /// <summary>
    /// برچسب
    /// </summary>
    public string[]? Tag { get; set; }
    /// <summary>
    /// آیدی معرف
    /// </summary>
    public Guid IntroducerId { get; set; }
    /// <summary>
    /// آیدی بیمه
    /// </summary>
    public Guid InsuranceId { get; set; }
    /// <summary>
    /// نوع معرف
    /// </summary>
    public IntroducerType? IntroducerType { get; set; }
    /// <summary>
    /// توضیحات پرونده
    /// </summary>
    public string FileDescription { get; set; } = string.Empty;
}