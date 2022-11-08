using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class PermissionConfiguration : BaseEntityTypeConfiguration<Permission, Guid>
    {
        private static Permission PermissionCreator(string guidId, string permissionCategoryId, string name, string normalizedName)
            => new() { Id = Guid.Parse(guidId), PermissionCategoryId = Guid.Parse(permissionCategoryId), Name = name, NormalizedName = normalizedName };

        public override void ConfigureEntity(EntityTypeBuilder<Permission> builder)
        {
            builder
                .HasData(new Permission[]
                {
                    PermissionCreator("8266f349-234c-400a-9670-4676b75d019c","7469a760-7fe8-43cf-9165-a8e415f91774","ReceptionEdit","ویرایش پذیرش"),
                    PermissionCreator("953ebbbe-a4f2-49b7-9273-8fceed61479e","7469a760-7fe8-43cf-9165-a8e415f91774","ReceptionDelete","حذف پذیرش"),
                    PermissionCreator("a3f8ca65-004e-4c5f-a3da-0c13b5b3d033","7469a760-7fe8-43cf-9165-a8e415f91774","ReceptionDateChange","تغییر تاریخ پذیرش"),
                    PermissionCreator("a46bf033-b50d-4e11-8c5d-0e404ed97b9f","7469a760-7fe8-43cf-9165-a8e415f91774","ReceptionDebtRegistration","ثبت بدهی"),
                    PermissionCreator("ac4c98c9-0295-4bea-b34b-19660f948852","7469a760-7fe8-43cf-9165-a8e415f91774","ReceptionReturnregistration","ثبت برگشتی"),
                    PermissionCreator("e4f9046b-7b60-4187-8f7d-50aeb32d7071","7469a760-7fe8-43cf-9165-a8e415f91774","ReceptionShiftChange","تغییر شیفت"),

                    PermissionCreator("f1568f21-659f-42d4-9a65-306acf0501c1","b15e5500-998f-40dc-80f2-983c5b1c1aba","FileEdit","ویرایش پرونده"),
                    PermissionCreator("520df499-cb60-45b7-9f48-a142694c9ff6","b15e5500-998f-40dc-80f2-983c5b1c1aba","FileDelete","حذف پرونده"),
                    PermissionCreator("583b93b7-60b0-418b-9f70-e3d22032a08a","b15e5500-998f-40dc-80f2-983c5b1c1aba","FileRegistration","ثبت پرونده"),
                    PermissionCreator("cd77a3e3-f0c1-427f-9dcb-e098f53167d4","b15e5500-998f-40dc-80f2-983c5b1c1aba","FilePrePaymentRegistration","ثبت مبلغ پیش پرداخت"),
                    PermissionCreator("086109f0-8508-472e-a644-12f40f32177f","b15e5500-998f-40dc-80f2-983c5b1c1aba","FilePrePaymentDateChange","تغییر تاریخ پیش پرداخت"),
                    PermissionCreator("1b419f29-ce34-4c4e-ad7c-2804d8a6e15a","b15e5500-998f-40dc-80f2-983c5b1c1aba","FileExcel","خروجی اکسل پرونده"),
                    PermissionCreator("00826518-1bb8-4052-b9e1-0e64a5a6f7be","b15e5500-998f-40dc-80f2-983c5b1c1aba","FileChangeUser","تغییر کاربر پرونده"),
                    PermissionCreator("362754cf-e06e-466d-9d90-473360ec4308","b15e5500-998f-40dc-80f2-983c5b1c1aba","FilePermissionPatientNumber","دسترسی به شمار تلفن بیمار"),

                    PermissionCreator("604688bf-66d9-4bf1-b5e0-9b6f3fff7073","365298ad-1986-45c5-a74b-3173b6f90655","DoctorVisitRegistration","ثبت ویزیت"),
                    PermissionCreator("5657088d-1870-4de4-918d-3698e92e7f22","365298ad-1986-45c5-a74b-3173b6f90655","DoctorVisitEdit","ویرایش ویزیت"),
                    PermissionCreator("42baa433-f392-4489-8f4e-d77b1c27978b","365298ad-1986-45c5-a74b-3173b6f90655","DoctorVisitDelete","حذف ویزیت"),
                    PermissionCreator("b968cb11-43a4-4bbe-a3ca-95a7d1bb9daa","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionPatientHistory","سوابق بیمار"),
                    PermissionCreator("b9e66192-1c2a-4dbf-97f6-79a6d861a872","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionLightPen","دسترسی به قلم نوری"),
                    PermissionCreator("94195d88-bd36-49b4-8bba-9f575e498b8d","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionPictures","دسترسی به تصاویر"),
                    PermissionCreator("e34710cc-d5eb-4a99-acaf-771a6dcd00f3","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionCommitments","دسترسی به تعهدنامه ها"),
                    PermissionCreator("59114568-3b0c-44a9-950c-565fd6f67e23","365298ad-1986-45c5-a74b-3173b6f90655","DoctorRegisteredVisitChange","دسترسی تغییر در ویزیت های ثبت شده"),
                    PermissionCreator("99f8a553-8445-4d35-bb0e-6e3331353578","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionForms","دسترسی فرم ها"),
                    PermissionCreator("b43130fb-edbe-41a6-b4e0-07278191505c","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionPrescription","دسترسی به نسخه نویسی"),
                });
        }
    }
}



///// <summary>
///// فعال یا غیرفعال بودن پذیرش
///// </summary>
//public bool IsReceptionPermissionActive { get; set; } = false;
///// <summary>
///// ویرایش پذیرش
///// </summary>
//public bool ReceptionEdit { get; set; } = false;
///// <summary>
///// حذف پذیرش
///// </summary>
//public bool ReceptionDelete { get; set; } = false ;
///// <summary>
///// تغییر تاریخ پذیرش
///// </summary>
//public bool ReceptionDateChange { get; set; } = false ;
///// <summary>
///// ثبت بدهی
///// </summary>
//public bool ReceptionDebtRegistration { get; set; } = false;
///// <summary>
///// ثبت برگشتی
///// </summary>
//public bool ReceptionReturnregistration { get; set; } = false;
///// <summary>
///// تغییر شیفت
///// </summary>
//public bool ReceptionShiftChange { get; set; } = false;



///// <summary>
///// فعال یا غیرفعال پرونده
///// </summary>
//public bool IsFilePermissionActive { get; set; } = false;
///// <summary>
///// ویرایش پرونده
///// </summary>
//public bool FileEdit { get; set; } = false;
///// <summary>
///// حذف پرونده
///// </summary>
//public bool FileDelete { get; set; } = false;
///// <summary>
///// ثبت پرونده
///// </summary>
//public bool FileRegistration { get; set; } = false;
///// <summary>
///// ثبت مبلغ پیش پرداخت
///// </summary>
//public bool FileRegistrationAdvancePayment { get; set; } = false;
///// <summary>
///// تغییر تاریخ پیش پرداخت
///// </summary>
//public bool FileChangeDateAdvancePayment { get; set; } = false;
///// <summary>
///// خروجی اکسل پرونده ها
///// </summary>
//public bool FileExcel { get; set; } = false;
///// <summary>
///// تغییر کاربر پرونده
///// </summary>
//public bool FileChangeUser { get; set; } = false;
///// <summary>
///// عدم دسترسی به شماره تلفن بیمار
///// </summary>
//public bool FilePermissionPatientNumber { get; set; } = false;



///// <summary>
///// فعال یا غیرفعال پزشکان
///// </summary>
//public bool IsDoctorPermissionActive { get; set; } = false;
///// <summary>
///// ثبت ویزیت
///// </summary>
//public bool DoctorVisitRegistration { get; set; } = false;
///// <summary>
///// ویرایش ویزیت
///// </summary>
//public bool DoctorVisitEdit { get; set; } = false;
///// <summary>
///// حذف ویزیت
///// </summary>
//public bool DoctorVisitDelete { get; set; } = false;
///// <summary>
///// دسترسی به سوابق بیمار
///// </summary>
//public bool DoctorPermissionPatientHistory { get; set; } = false;
///// <summary>
///// دسترسی به قلم نوری
///// </summary>
//public bool DoctorPermissionLightPen { get; set; } = false;
///// <summary>
///// دسترسی به تصاویر
///// </summary>
//public bool DoctorPermissionPictures { get; set; } = false;
///// <summary>
///// دسترسی به تعهدنامه ها
///// </summary>
//public bool DoctorPermissionCommitments { get; set; } = false;
///// <summary>
///// عدم تغییر در ویزیت های ثبت شده دیگران 
///// </summary>
//public bool DoctorChangeOthersVisit { get; set; } = false;
///// <summary>
///// دسترسی به فرم ها 
///// </summary>
//public bool DoctorPermissionForms { get; set; } = false;
///// <summary>
///// دسترسی به نسخه نویسی 
///// </summary>
//public bool DoctorPermissionPrescription { get; set; } = false;

/*----------------------------------------------------------------*/

///// <summary>
///// فعال یا غیرفعال گزارشات
///// </summary>
//public bool IsReportPermissionActive { get; set; } = false;
///// <summary>
///// صندوق روزانه
///// </summary>
//public bool ReportDailyCash { get; set; } = false;
///// <summary>
///// گزارش مالی
///// </summary>
//public bool ReportFinancial { get; set; } = false;
///// <summary>
///// گزارش هزینه ها 
///// </summary>
//public bool ReportExpense { get; set; } = false;
///// <summary>
///// گزارشات بدهکاران 
///// </summary>
//public bool ReportDebtors { get; set; } = false;
///// <summary>
///// گزارش بیعانه 
///// </summary>
//public bool ReportDeposit { get; set; } = false;
///// <summary>
///// گزارش معرف ها  
///// </summary>
//public bool ReportIntroducers { get; set; } = false;
///// <summary>
///// گزارش اقساط  
///// </summary>
//public bool ReportInstallment { get; set; } = false;
///// <summary>
///// گزارش نسخ الکترونیک  
///// </summary>
//public bool ReportElectronicPrescription { get; set; } = false;
///// <summary>
///// گزارش وضعیت مراجعه بیماران  
///// </summary>
//public bool ReportStatuseofPatients { get; set; } = false;
///// <summary>
///// گزارش خدمات ارائه شده  
///// </summary>
//public bool ReportServicesProvided { get; set; } = false;
///// <summary>
///// گزارش وقتدهی  
///// </summary>
//public bool ReportTimimg { get; set; } = false;
///// <summary>
///// گزارش کار کرد پزشکان   
///// </summary>
//public bool ReportDoctorsPerformancd { get; set; } = false;
///// <summary>
/////  گزارش کارکرد کارشناس   
///// </summary>
//public bool ReportExpertsPerformancd { get; set; } = false;
///// <summary>
/////  گزارش بیمه ها و گزارش بیمه تکمیلی   
///// </summary>
//public bool ReportInsurances { get; set; } = false;
///// <summary>
/////  گزارش ارسال نسخ بیمه   
///// </summary>
//public bool ReportInsuranceCopies { get; set; } = false;
///// <summary>
/////  گزارش برگشتی ها   
///// </summary>
//public bool ReportReturns { get; set; } = false;
///// <summary>
/////  گزارش آماری ویزیت ها    
///// </summary>
//public bool ReportStaticticalVisits { get; set; } = false;
///// <summary>
/////   گزارش از فرم های اختصاصی    
///// </summary>
//public bool ReportSpecialForms { get; set; } = false;
///// <summary>
/////   گزارش انبار    
///// </summary>
//public bool ReportStore { get; set; } = false;



///// <summary>
///// فعال یا غیرفعال انبار
///// </summary>
//public bool IsStorePermissionActive { get; set; } = false;
///// <summary>
///// تعریف کالا
///// </summary>
//public bool StoreComidity { get; set; } = false;
///// <summary>
///// ثبت مصرفی
///// </summary>
//public bool StoreConsumerRegitration { get; set; } = false;
///// <summary>
///// انتقال کالا
///// </summary>
//public bool StoreComidityTrasportation { get; set; } = false;
///// <summary>
///// ثبت حواله
///// </summary>
//public bool StoreRemittanceRegitration { get; set; } = false;
///// <summary>
///// ویرایش حواله
///// </summary>
//public bool StoreRemittanceEdit { get; set; } = false;
///// <summary>
///// حذف حواله
///// </summary>
//public bool StoreRemittanceDelete { get; set; } = false;


///// <summary>
///// فعال یا غیرفعال وقتدهی
///// </summary>
//public bool IsTimingPermissionActive { get; set; } = false;
///// <summary>
///// دسترسی به ثبت وقت
///// </summary>
//public bool TimingRegistration { get; set; } = false;
///// <summary>
///// دسترسی به حذف وقت
///// </summary>
//public bool TimingDelete { get; set; } = false;
///// <summary>
///// دسترسی به کنسل وقت
///// </summary>
//public bool TimingCancelation { get; set; } = false;
///// <summary>
///// دسترسی به ثبت وقت برای پزشکان انتخاب شده
///// </summary>
//public bool TimingRegistrationforSelectedDoctors { get; set; } = false;


///// <summary>
///// فعال یا غیرفعال اطالعات پایه
///// </summary>
//public bool IsBasicInfoPermissionActive { get; set; } = false;


///// <summary>
///// فعال یا غیرفعال داشبورد
///// </summary>
//public bool IsDashboardPermissionActive { get; set; } = false;