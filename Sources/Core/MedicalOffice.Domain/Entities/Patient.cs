using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// بیمار
/// </summary>
public class Patient : BaseDomainEntity<Guid>
{
    /// <summary>
    /// شماره پرونده
    /// </summary>
    public int PatientCode { get; set; }
    /// <summary>
    /// شماره پرونده دستی
    /// </summary>
    public string FileNumber { get; set; } = string.Empty;
    /// <summary>
    /// شماره تلفن
    /// </summary>
    public string Mobile { get; set; } = string.Empty;
    /// <summary>
    /// اثر انگشت
    /// </summary>
    public byte[]? FingerPrint { get; set; } 
    /// <summary>
    /// مطب
    /// </summary>
    public Office? Office { get; set; }
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
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
    /// توضیحات پرونده
    /// </summary>
    public string FileDescription { get; set; } = string.Empty;
    /// <summary>
    /// بیمه
    /// </summary>
    public Insurance? Insurance { get; set; }
    /// <summary>
    /// آیدی بیمه
    /// </summary>
    public Guid InsuranceId { get; set; }
    /// <summary>
    /// آدرس ها
    /// </summary>
    public ICollection<PatientAddress> PatientAddresses { get; set; } = new List<PatientAddress>();
    /// <summary>
    /// فرم تعهدنامه ها
    /// </summary>
    public ICollection<FormCommitment> FormCommitments { get; set; } = new List<FormCommitment>();
    /// <summary>
    /// اطلاعات تماس
    /// </summary>
    public ICollection<PatientContact> PatientContacts { get; set; } = new List<PatientContact>();
    /// <summary>
    /// تگ ها
    /// </summary>
    public ICollection<PatientTag> PatientTags { get; set; } = new List<PatientTag>();
    /// <summary>
    /// پذیرش ها
    /// </summary>
    public ICollection<Reception> Receptions { get; set; } = new List<Reception>();
    /// <summary>
    /// وقت دهی ها
    /// </summary>
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    /// <summary>
    /// آیدی معرف
    /// </summary>
    public Guid IntroducerId { get; set; }
    /// <summary>
    /// نوع معرف
    /// </summary>
    public IntroducerType? IntroducerType { get; set; }
    /// <summary>
    /// تشخیص ها
    /// </summary>
    public ICollection<Diagnose> Diagnoses { get; set; } = new List<Diagnose>();
    /// <summary>
    /// معاینات کلی
    /// </summary>
    public ICollection<GeneralExamination> GeneralExaminations { get; set; } = new List<GeneralExamination>();
    /// <summary>
    /// حساسیت ها
    /// </summary>
    public ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
    /// <summary>
    /// سابقه دخانیات
    /// </summary>
    public ICollection<DrugAbuse> DrugAbuses { get; set; } = new List<DrugAbuse>();
    /// <summary>
    /// تجویز دارو ها
    /// </summary>
    public ICollection<DrugPrescription> DrugPrescriptions { get; set; } = new List<DrugPrescription>();
    /// <summary>
    /// معاینات فیزیکی
    /// </summary>
    public ICollection<PhysicalExam> PhysicalExams { get; set; } = new List<PhysicalExam>();
    /// <summary>
    /// پی ام اچ ها
    /// </summary>
    public ICollection<PMH> PMHs { get; set; } = new List<PMH>();
    /// <summary>
    /// دارو های روتین
    /// </summary>
    public ICollection<RoutineMedication> RoutineMedications { get; set; } = new List<RoutineMedication>();
    /// <summary>
    /// سابقه اجتماعی
    /// </summary>
    public ICollection<SocialHistory> SocialHistories { get; set; } = new List<SocialHistory>();
    /// <summary>
    /// اقدامات درمانی
    /// </summary>
    public ICollection<MedicalAction> MedicalActions { get; set; } = new List<MedicalAction>();
    /// <summary>
    /// لیست فایل
    /// </summary>
    public ICollection<PatientFiles> PatientFiless { get; set; } = new List<PatientFiles>();
    /// <summary>
    /// لیست استعلاجی
    /// </summary>
    public ICollection<PatientIllnessForm> PatientIllnessForms { get; set; } = new List<PatientIllnessForm>();
    /// <summary>
    /// لیست ارجاع
    /// </summary>
    public ICollection<PatientReferralForm> PatientReferralForms { get; set; } = new List<PatientReferralForm>();
    /// <summary>
    /// لیست فرم تعهدنامه
    /// </summary>
    public ICollection<PatientCommitmentForm> PatientCommitmentForms { get; set; } = new List<PatientCommitmentForm>();
    /// <summary>
    /// لیست عکس ها
    /// </summary>
    public ICollection<Picture>? Pictures { get; set; }
}
