using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.PermissionDTO
{
    public class PermissionListDTO
    {
        /// <summary>
        /// آیدی مطب
        /// </summary>
        public Guid? OfficeId { get; set; }
        /// <summary>
        /// آیدی کاربر
        /// </summary>
        public Guid? UserOfficeRoleId { get; set; }



        /// <summary>
        /// فعال یا غیرفعال بودن پذیرش
        /// </summary>
        public bool IsReceptionPermissionActive { get; set; } = false;
        /// <summary>
        /// ویرایش پذیرش
        /// </summary>
        public bool ReceptionEdit { get; set; } = false;
        /// <summary>
        /// حذف پذیرش
        /// </summary>
        public bool ReceptionDelete { get; set; } = false;
        /// <summary>
        /// تغییر تاریخ پذیرش
        /// </summary>
        public bool ReceptionDateChange { get; set; } = false;
        /// <summary>
        /// ثبت بدهی
        /// </summary>
        public bool ReceptionDebtRegistration { get; set; } = false;
        /// <summary>
        /// ثبت برگشتی
        /// </summary>
        public bool ReceptionReturnregistration { get; set; } = false;
        /// <summary>
        /// تغییر شیفت
        /// </summary>
        public bool ReceptionShiftChange { get; set; } = false;



        /// <summary>
        /// فعال یا غیرفعال پرونده
        /// </summary>
        public bool IsFilePermissionActive { get; set; } = false;
        /// <summary>
        /// ویرایش پرونده
        /// </summary>
        public bool FileEdit { get; set; } = false;
        /// <summary>
        /// حذف پرونده
        /// </summary>
        public bool FileDelete { get; set; } = false;
        /// <summary>
        /// ثبت پرونده
        /// </summary>
        public bool FileRegistration { get; set; } = false;
        /// <summary>
        /// ثبت مبلغ پیش پرداخت
        /// </summary>
        public bool FileRegistrationAdvancePayment { get; set; } = false;
        /// <summary>
        /// تغییر تاریخ پیش پرداخت
        /// </summary>
        public bool FileChangeDateAdvancePayment { get; set; } = false;
        /// <summary>
        /// خروجی اکسل پرونده ها
        /// </summary>
        public bool FileExcel { get; set; } = false;
        /// <summary>
        /// تغییر کاربر پرونده
        /// </summary>
        public bool FileChangeMedicalStaff { get; set; } = false;
        /// <summary>
        /// عدم دسترسی به شماره تلفن بیمار
        /// </summary>
        public bool FilePermissionPatientNumber { get; set; } = false;



        /// <summary>
        /// فعال یا غیرفعال پزشکان
        /// </summary>
        public bool IsDoctorPermissionActive { get; set; } = false;
        /// <summary>
        /// ثبت ویزیت
        /// </summary>
        public bool DoctorVisitRegistration { get; set; } = false;
        /// <summary>
        /// ویرایش ویزیت
        /// </summary>
        public bool DoctorVisitEdit { get; set; } = false;
        /// <summary>
        /// حذف ویزیت
        /// </summary>
        public bool DoctorVisitDelete { get; set; } = false;
        /// <summary>
        /// دسترسی به سوابق بیمار
        /// </summary>
        public bool DoctorPermissionPatientHistory { get; set; } = false;
        /// <summary>
        /// دسترسی به قلم نوری
        /// </summary>
        public bool DoctorPermissionLightPen { get; set; } = false;
        /// <summary>
        /// دسترسی به تصاویر
        /// </summary>
        public bool DoctorPermissionPictures { get; set; } = false;
        /// <summary>
        /// دسترسی به تعهدنامه ها
        /// </summary>
        public bool DoctorPermissionCommitments { get; set; } = false;
        /// <summary>
        /// عدم تغییر در ویزیت های ثبت شده دیگران 
        /// </summary>
        public bool DoctorChangeOthersVisit { get; set; } = false;
        /// <summary>
        /// دسترسی به فرم ها 
        /// </summary>
        public bool DoctorPermissionForms { get; set; } = false;
        /// <summary>
        /// دسترسی به نسخه نویسی 
        /// </summary>
        public bool DoctorPermissionPrescription { get; set; } = false;


        /// <summary>
        /// فعال یا غیرفعال گزارشات
        /// </summary>
        public bool IsReportPermissionActive { get; set; } = false;
        /// <summary>
        /// صندوق روزانه
        /// </summary>
        public bool ReportDailyCash { get; set; } = false;
        /// <summary>
        /// گزارش مالی
        /// </summary>
        public bool ReportFinancial { get; set; } = false;
        /// <summary>
        /// گزارش هزینه ها 
        /// </summary>
        public bool ReportExpense { get; set; } = false;
        /// <summary>
        /// گزارشات بدهکاران 
        /// </summary>
        public bool ReportDebtors { get; set; } = false;
        /// <summary>
        /// گزارش بیعانه 
        /// </summary>
        public bool ReportDeposit { get; set; } = false;
        /// <summary>
        /// گزارش معرف ها  
        /// </summary>
        public bool ReportIntroducers { get; set; } = false;
        /// <summary>
        /// گزارش اقساط  
        /// </summary>
        public bool ReportInstallment { get; set; } = false;
        /// <summary>
        /// گزارش نسخ الکترونیک  
        /// </summary>
        public bool ReportElectronicPrescription { get; set; } = false;
        /// <summary>
        /// گزارش وضعیت مراجعه بیماران  
        /// </summary>
        public bool ReportStatuseofPatients { get; set; } = false;
        /// <summary>
        /// گزارش خدمات ارائه شده  
        /// </summary>
        public bool ReportServicesProvided { get; set; } = false;
        /// <summary>
        /// گزارش وقتدهی  
        /// </summary>
        public bool ReportTimimg { get; set; } = false;
        /// <summary>
        /// گزارش کار کرد پزشکان   
        /// </summary>
        public bool ReportDoctorsPerformancd { get; set; } = false;
        /// <summary>
        ///  گزارش کارکرد کارشناس   
        /// </summary>
        public bool ReportExpertsPerformancd { get; set; } = false;
        /// <summary>
        ///  گزارش بیمه ها و گزارش بیمه تکمیلی   
        /// </summary>
        public bool ReportInsurances { get; set; } = false;
        /// <summary>
        ///  گزارش ارسال نسخ بیمه   
        /// </summary>
        public bool ReportInsuranceCopies { get; set; } = false;
        /// <summary>
        ///  گزارش برگشتی ها   
        /// </summary>
        public bool ReportReturns { get; set; } = false;
        /// <summary>
        ///  گزارش آماری ویزیت ها    
        /// </summary>
        public bool ReportStaticticalVisits { get; set; } = false;
        /// <summary>
        ///   گزارش از فرم های اختصاصی    
        /// </summary>
        public bool ReportSpecialForms { get; set; } = false;
        /// <summary>
        ///   گزارش انبار    
        /// </summary>
        public bool ReportStore { get; set; } = false;



        /// <summary>
        /// فعال یا غیرفعال انبار
        /// </summary>
        public bool IsStorePermissionActive { get; set; } = false;
        /// <summary>
        /// تعریف کالا
        /// </summary>
        public bool StoreComidity { get; set; } = false;
        /// <summary>
        /// ثبت مصرفی
        /// </summary>
        public bool StoreConsumerRegitration { get; set; } = false;
        /// <summary>
        /// انتقال کالا
        /// </summary>
        public bool StoreComidityTrasportation { get; set; } = false;
        /// <summary>
        /// ثبت حواله
        /// </summary>
        public bool StoreRemittanceRegitration { get; set; } = false;
        /// <summary>
        /// ویرایش حواله
        /// </summary>
        public bool StoreRemittanceEdit { get; set; } = false;
        /// <summary>
        /// حذف حواله
        /// </summary>
        public bool StoreRemittanceDelete { get; set; } = false;


        /// <summary>
        /// فعال یا غیرفعال وقتدهی
        /// </summary>
        public bool IsTimingPermissionActive { get; set; } = false;
        /// <summary>
        /// دسترسی به ثبت وقت
        /// </summary>
        public bool TimingRegistration { get; set; } = false;
        /// <summary>
        /// دسترسی به حذف وقت
        /// </summary>
        public bool TimingDelete { get; set; } = false;
        /// <summary>
        /// دسترسی به کنسل وقت
        /// </summary>
        public bool TimingCancelation { get; set; } = false;
        /// <summary>
        /// دسترسی به ثبت وقت برای پزشکان انتخاب شده
        /// </summary>
        public bool TimingRegistrationforSelectedDoctors { get; set; } = false;


        /// <summary>
        /// فعال یا غیرفعال اطالعات پایه
        /// </summary>
        public bool IsBasicInfoPermissionActive { get; set; } = false;


        /// <summary>
        /// فعال یا غیرفعال داشبورد
        /// </summary>
        public bool IsDashboardPermissionActive { get; set; } = false;
    }
}
