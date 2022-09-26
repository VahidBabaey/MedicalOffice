using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

public class PhysicalExam : BaseDomainEntity<Guid>
{
    /// <summary>
    /// بیمار
    /// </summary>
    public Patient? Patient { get; set; }
    /// <summary>
    /// آیدی بیمار
    /// </summary>
    public Guid PatientId { get; set; }
    /// <summary>
    /// تاریخ معاینه
    /// </summary>
    public string ExamDate { get; set; } = string.Empty;
    /// <summary>
    /// فشار خون مینیمم
    /// </summary>
    public float BloodPressureMin { get; set; }
    /// <summary>
    /// فشار خون ماکسیمم
    /// </summary>
    public float BloodPressureMax { get; set; }
    /// <summary>
    /// نبض
    /// </summary>
    public float Pulse { get; set; }
    /// <summary>
    /// اشباع اکسیژن مویرگی
    /// </summary>
    public float SPO2 { get; set; }
    /// <summary>
    /// دور کمر
    /// </summary>
    public float Waist { get; set; }
    /// <summary>
    /// تنفس
    /// </summary>
    public float Breathing { get; set; }
    /// <summary>
    /// تب
    /// </summary>
    public float Heat { get; set; }
    /// <summary>
    /// وزن
    /// </summary>
    public float Weight { get; set; }
    /// <summary>
    /// قد
    /// </summary>
    public float Height { get; set; }
    /// <summary>
    /// قند خون
    /// </summary>
    public float FastBloodSugar { get; set; }
    /// <summary>
    /// فشار گاز ریوی
    /// </summary>
    public float PO2 { get; set; }
    /// <summary>
    /// شاخص توده بدنی
    /// </summary>
    public float BMI { get; set; }
    /// <summary>
    /// مساحت بدن
    /// </summary>
    public float BSA { get; set; }
    /// <summary>
    /// وضعیت رژیم
    /// </summary>
    public bool Diet { get; set; }
}
