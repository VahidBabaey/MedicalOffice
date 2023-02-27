using MedicalOffice.Domain.Entities;
using MedicalOffice.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        private static Permission PermissionCreator(string guidId, string? parentId, string name, string persianName, bool isShown = true)
            => new() { Id = Guid.Parse(guidId), ParentId = parentId != null ? Guid.Parse(parentId) : null, Name = name, PersianName = persianName, IsShown = isShown };

        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder
                .HasData(new Permission[]
                {
                    PermissionCreator("7469a760-7fe8-43cf-9165-a8e415f91774",null,"ReceptionPermission","دسترسی پذیرش"),
                    PermissionCreator("8266f349-234c-400a-9670-4676b75d019c","7469a760-7fe8-43cf-9165-a8e415f91774","ReceptionEdit","ویرایش پذیرش"),
                    PermissionCreator("953ebbbe-a4f2-49b7-9273-8fceed61479e","7469a760-7fe8-43cf-9165-a8e415f91774","ReceptionDelete","حذف پذیرش"),
                    PermissionCreator("a3f8ca65-004e-4c5f-a3da-0c13b5b3d033","7469a760-7fe8-43cf-9165-a8e415f91774","ReceptionDateChange","تغییر تاریخ پذیرش"),
                    PermissionCreator("a46bf033-b50d-4e11-8c5d-0e404ed97b9f","7469a760-7fe8-43cf-9165-a8e415f91774","ReceptionDebtRegistration","ثبت بدهی"),
                    PermissionCreator("ac4c98c9-0295-4bea-b34b-19660f948852","7469a760-7fe8-43cf-9165-a8e415f91774","ReceptionReturnregistration","ثبت برگشتی"),
                    PermissionCreator("e4f9046b-7b60-4187-8f7d-50aeb32d7071","7469a760-7fe8-43cf-9165-a8e415f91774","ReceptionShiftChange","تغییر شیفت"),

                    PermissionCreator("b15e5500-998f-40dc-80f2-983c5b1c1aba",null,"FilePermission","دسترسی پرونده"),
                    PermissionCreator("ea53dd69-35c5-43f7-a0aa-be02f24bfa71","b15e5500-998f-40dc-80f2-983c5b1c1aba","AllFilesPermission","دسترسی به کل پرونده ها"),
                    PermissionCreator("f1568f21-659f-42d4-9a65-306acf0501c1","b15e5500-998f-40dc-80f2-983c5b1c1aba","FileEdit","ویرایش پرونده"),
                    PermissionCreator("520df499-cb60-45b7-9f48-a142694c9ff6","b15e5500-998f-40dc-80f2-983c5b1c1aba","FileDelete","حذف پرونده"),
                    PermissionCreator("583b93b7-60b0-418b-9f70-e3d22032a08a","b15e5500-998f-40dc-80f2-983c5b1c1aba","FileRegistration","ثبت پرونده"),
                    PermissionCreator("cd77a3e3-f0c1-427f-9dcb-e098f53167d4","b15e5500-998f-40dc-80f2-983c5b1c1aba","FilePrePaymentRegistration","ثبت مبلغ پیش پرداخت"),
                    PermissionCreator("086109f0-8508-472e-a644-12f40f32177f","b15e5500-998f-40dc-80f2-983c5b1c1aba","FilePrePaymentDateChange","تغییر تاریخ پیش پرداخت"),
                    PermissionCreator("1b419f29-ce34-4c4e-ad7c-2804d8a6e15a","b15e5500-998f-40dc-80f2-983c5b1c1aba","FileExcel","خروجی اکسل پرونده"),
                    PermissionCreator("00826518-1bb8-4052-b9e1-0e64a5a6f7be","b15e5500-998f-40dc-80f2-983c5b1c1aba","FileChangeUser","تغییر کاربر پرونده"),
                    PermissionCreator("362754cf-e06e-466d-9d90-473360ec4308","b15e5500-998f-40dc-80f2-983c5b1c1aba","FilePermissionPatientNumber","دسترسی به شمار تلفن بیمار"),
                    PermissionCreator("8384e4ab-3784-4a13-b11a-27e43be3a827","b15e5500-998f-40dc-80f2-983c5b1c1aba","FilePermissionPictures"," دسترسی به تصاویر پرونده"),

                    PermissionCreator("365298ad-1986-45c5-a74b-3173b6f90655",null,"DoctorsPermission","دسترسی پزشکان"),
                    PermissionCreator("604688bf-66d9-4bf1-b5e0-9b6f3fff7073","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionVisitRegistration","ثبت ویزیت"),
                    PermissionCreator("5657088d-1870-4de4-918d-3698e92e7f22","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionVisitEdit","ویرایش ویزیت"),
                    PermissionCreator("42baa433-f392-4489-8f4e-d77b1c27978b","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionVisitDelete","حذف ویزیت"),
                    PermissionCreator("b968cb11-43a4-4bbe-a3ca-95a7d1bb9daa","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionPatientHistory","سوابق بیمار"),
                    PermissionCreator("b9e66192-1c2a-4dbf-97f6-79a6d861a872","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionLightPen","دسترسی به قلم نوری"),
                    PermissionCreator("94195d88-bd36-49b4-8bba-9f575e498b8d","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionPictures"," دسترسی به تصاویر ویزیت"),
                    PermissionCreator("e34710cc-d5eb-4a99-acaf-771a6dcd00f3","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionCommitments","دسترسی به تعهدنامه ها"),
                    PermissionCreator("59114568-3b0c-44a9-950c-565fd6f67e23","365298ad-1986-45c5-a74b-3173b6f90655","DoctorOthersRegisteredVisitChange","دسترسی تغییر در ویزیت های ثبت شده دیگران"),
                    PermissionCreator("99f8a553-8445-4d35-bb0e-6e3331353578","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionForms","دسترسی فرم ها"),
                    PermissionCreator("b43130fb-edbe-41a6-b4e0-07278191505c","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionPrescription","دسترسی به نسخه نویسی"),
                    PermissionCreator("9729da56-1152-4a10-8817-3f2b87a6f4a5","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionFileBrief","دسترسی به خلاصه پرونده"),
                    PermissionCreator("5921e3d9-33cb-40c3-95ec-aa30f27d8488","365298ad-1986-45c5-a74b-3173b6f90655","DoctorPermissionPMH","دسترسی به PMH"),

                    PermissionCreator("05a066f7-0a5e-4e70-a382-65e18453ae46",null,"ReportPermission","دسترسی گزارشات"),
                    PermissionCreator("3e7c5991-89d9-4a98-967e-71e68393ea3b","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportDailyCash","صندوق روزانه"),
                    PermissionCreator("789a92c9-28b4-4200-b9f0-e1ebe8b9a7bf","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportFinancial","گزارش مالی"),
                    PermissionCreator("037a2d46-d42c-406d-b14b-c7987a120c6b","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportExpense","گزارش هزینه ها"),
                    PermissionCreator("815f5c0d-753c-4097-be96-4056ca5b54a7","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportDebtors","گزارشات بدهکاران"),
                    PermissionCreator("6a9f0d7c-dcc4-4752-8614-c372bd4210c9","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportDeposit","گزارش بیعانه"),
                    PermissionCreator("37b7b088-52a7-4788-955f-bb3d1149a3ea","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportIntroducers","گزارش معرف ها"),
                    PermissionCreator("11097b06-4d28-4cfc-8f22-a8fe9ab9aa26","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportInstallment","گزارش اقساط"),
                    PermissionCreator("d196fef8-e432-4218-bb45-59d82f8f7aec","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportElectronicPrescription","گزارش نسخ الکترونیک"),
                    PermissionCreator("f0a6cebc-72b1-41a6-b296-7eb965456a12","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportStatuseofPatients","گزارش وضعیت مراجعه بیماران"),
                    PermissionCreator("c1bcaa75-ec51-45c9-b90c-3b82783560d9","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportServicesProvided","گزارش خدمات ارائه شده"),
                    PermissionCreator("23fb6e24-0e15-42ea-884a-2f30137b6db1","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportAppointment","گزارش وقتدهی"),
                    PermissionCreator("cf61024a-089e-4020-89c7-69898deeb8ee","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportDoctorsPerformancd","گزارش کار کرد پزشکان"),
                    PermissionCreator("39232a82-7be1-4822-97a1-fe96598a78b0","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportExpertsPerformancd","گزارش کارکرد کارشناس"),
                    PermissionCreator("4ccfc4f8-442f-4bb6-ab0d-41da4b3ac7b6","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportInsurances","گزارش بیمه ها و گزارش بیمه تکمیلی"),
                    PermissionCreator("0c3e0956-1350-4b0e-969d-3b0f5781ebae","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportInsuranceVersions","گزارش ارسال نسخ بیمه"),
                    PermissionCreator("fa9ec427-4953-4406-8806-7a03a0ddb90c","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportReturns","گزارش برگشتی ها"),
                    PermissionCreator("dd63bb5f-c4a7-4d1e-9be5-76ec499f2e98","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportStaticticalVisits","گزارش آماری ویزیت ها"),
                    PermissionCreator("e7ec3e67-8ba8-46d4-8a6c-9f003c264978","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportSpecialForms","گزارش از فرم های اختصاصی"),
                    PermissionCreator("a952610e-01a1-4df8-a50b-87c750a8ce39","05a066f7-0a5e-4e70-a382-65e18453ae46","ReportStore","گزارش انبار"),

                    PermissionCreator("d5eccfd3-a6c9-422b-835a-a77f0295481f",null,"StorePermission","دسترسی انبار"),
                    PermissionCreator("09b7d194-d6b3-43fb-9591-3b5fb9a2f145","d5eccfd3-a6c9-422b-835a-a77f0295481f","StoreDefinition","تعریف انبار"),
                    PermissionCreator("549cc91b-62e2-4bcc-b428-2c7ca785167a","d5eccfd3-a6c9-422b-835a-a77f0295481f","StoreComidityDefinition","تعریف کالا"),
                    PermissionCreator("10ec79b9-dd1a-427f-b0bb-86963c29045a","d5eccfd3-a6c9-422b-835a-a77f0295481f","StoreConsumerRegitration","ثبت مصرفی"),
                    PermissionCreator("4d32b6dc-f206-451a-9425-dbab00609b66","d5eccfd3-a6c9-422b-835a-a77f0295481f","StoreComidityTrasportation","انتقال کالا"),
                    PermissionCreator("61c3d629-76bb-4755-8eba-891b833917fc","d5eccfd3-a6c9-422b-835a-a77f0295481f","StoreRemittanceRegitration","ثبت حواله"),
                    PermissionCreator("08a07881-ff1a-4975-95d0-96ee3cc91c74","d5eccfd3-a6c9-422b-835a-a77f0295481f","StoreRemittanceEdit","ویرایش حواله"),
                    PermissionCreator("0aafb075-aa20-4fff-9782-58b6a74928ef","d5eccfd3-a6c9-422b-835a-a77f0295481f","StoreRemittanceDelete","حذف حواله"),

                    PermissionCreator("23bc31a3-6542-43d7-a4e8-6a953415e0d0",null,"AppointmentPermission","دسترسی وقت دهی"),
                    PermissionCreator("a438dcc1-8a04-4859-b224-a1ec6235bad1","23bc31a3-6542-43d7-a4e8-6a953415e0d0","AppointmentRegistration","دسترسی به ثبت وقت"),
                    PermissionCreator("077672d1-4a6c-4cc5-947e-7bc36954ee41","23bc31a3-6542-43d7-a4e8-6a953415e0d0","AppointmentDelete","دسترسی به حذف وقت"),
                    PermissionCreator("c54c7024-87e7-43de-a5b1-2763296be889","23bc31a3-6542-43d7-a4e8-6a953415e0d0","AppointmentCancelation","دسترسی به کنسل وقت"),
                    PermissionCreator("b9868f8e-1f05-4c89-a3f3-83c440961705","23bc31a3-6542-43d7-a4e8-6a953415e0d0","AppointmentRegistrationforSelectedDoctors","دسترسی به ثبت وقت برای پزشکان انتخاب شده"),

                    PermissionCreator("529e3ed5-51ea-4411-8fbb-ab62e99f7691",null,"BasicInfoPermission","دسترسی اطلاعات پایه"),
                    PermissionCreator("a23e6968-b82a-404c-92ec-16e8ddb7651f","529e3ed5-51ea-4411-8fbb-ab62e99f7691","BasicInfoPermission","دسترسی اطلاعات پایه", false),

                    PermissionCreator("9301e02e-c11d-4c8f-bc72-c40c6322eebb",null,"DashboardPermission","دسترسی به داشبورد"),
                    PermissionCreator("7dbb0a47-6aa3-442e-959a-e4d5fffeeac4","9301e02e-c11d-4c8f-bc72-c40c6322eebb","DashboardPermission","دسترسی به داشبورد", false),

                    PermissionCreator("3f75033b-be8a-47e7-b86a-fa67c48785dc",null,"PreparedPatternsPermission","دسترسی به الگوهای آماده"),
                    PermissionCreator("ef25d083-6049-4d97-a0b6-b9f34c37b6af","3f75033b-be8a-47e7-b86a-fa67c48785dc","PreparedPatternsPermission","دسترسی به الگوهای آماده", false),

                    PermissionCreator("202eafde-1b56-428b-9b0b-60a8d5efe812",null,"SupportPermission","دسترسی پشتیبانی",false),
                    PermissionCreator("74d411ab-8667-4801-b412-7c015d556466","202eafde-1b56-428b-9b0b-60a8d5efe812","SupportPermission","دسترسی پشتیبانی",false),

                    PermissionCreator("0f8e8881-c090-4d01-9ba7-c2fdb42549b3",null,"TodayPatientPermission","دسترسی بیماران امروز"),
                    PermissionCreator("931f674f-c2a5-434b-97b1-438a9131e55d","0f8e8881-c090-4d01-9ba7-c2fdb42549b3","TodayPatientPermission","دسترسی بیماران امروز",false),
                });
        }
    }
}