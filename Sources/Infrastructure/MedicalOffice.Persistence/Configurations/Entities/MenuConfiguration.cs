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
    public class MenuConfiguration : BaseEntityTypeConfiguration<Menu, Guid>
    {
        private static Menu MenuCreator(string guidId, string name, string persianName, string parent = null, string link = null)
         => new() { Id = Guid.Parse(guidId), Name = name, PersianName = persianName, ParentId = Guid.Parse(parent), Link = link };

        public override void ConfigureEntity(EntityTypeBuilder<Menu> builder)
        {
            builder.HasData(new Menu[]
            {
                MenuCreator("","File","پرونده"),
                MenuCreator("","CreateFile","تشکیل پرونده ",""),
                MenuCreator("","AdvanceSearchFile","جستو جوی پیشرفته",""),

                MenuCreator("","Reception","پذیرش"),

                MenuCreator("","TodayPatient","بیماران امروز"),

                MenuCreator("","Appointment","وقت دهی "),
                MenuCreator("","AppointmentSetting","تنظیمات وقت دهی",""),
                MenuCreator("","Appointment","وقت دهی",""),

                MenuCreator("","Visit","ویزیت"),
                MenuCreator("","Visit","ویزیت",""),
                MenuCreator("","PMH","PMH",""),
                MenuCreator("","Paraclinic","پاراکلینیک",""),
                MenuCreator("","Graphs","نمودارها",""),
                MenuCreator("","Forms","فرم ها",""),
                MenuCreator("","َAtFirstLook","در یک نگاه",""),

                MenuCreator("","ElectronicPrescribing","نسخه نویسی الکترونیک"),
                
                MenuCreator("","LightPen","قلم نوری"),
                
                MenuCreator("","reports","گزارش ها"),

                MenuCreator("","Warehousing","انبارداری"),
                MenuCreator("","reports","تعاریف انبار",""),
                MenuCreator("","reports","حواله ورود",""),
                MenuCreator("","reports","حواله خروج",""),
                MenuCreator("","reports","گردش کالا",""),
                MenuCreator("","reports","موجودی کالا",""),
                MenuCreator("","reports","گزارشات انبار",""),

                MenuCreator("","SMS","پیامک"),
                MenuCreator("","SMSSharge","شارژ پیامک",""),
                MenuCreator("","Settings","تنظیمات",""),
                MenuCreator("","Sents","ارسال شده ها",""),

                MenuCreator("","BasicInfo","اطلاعات پایه"),
                MenuCreator("","OfficeInfo","اطلاعات مطب",""),
                MenuCreator("","DefinitionOfMedicalStaffs","تعریف کادر درمان",""),
                MenuCreator("","DefinitionOfInsurance","تعریف بیمه ها",""),
                MenuCreator("","DefinitionOfSection","تعریف بخش",""),
                MenuCreator("","DefinitionOfServices","تعریف خدمات",""),
                MenuCreator("","DefinitionOfShifts","تعریف شیفت",""),
                MenuCreator("","DefinitionOfMembership","تعریف عضویت",""),
                MenuCreator("","DefinitionOfKCoefficient","تعریف ضریب K",""),
                MenuCreator("","DefinitionOfDrug","تعریف دارو",""),
                MenuCreator("","DefinitionOfDrugInteractions","تعریف تداخلات دارو",""),
                MenuCreator("","DefinitionOfExperiment","تعریف آزمایش",""),
                MenuCreator("","DefinitionOfThematicBase","تعریف پایه موضوعی",""),
                MenuCreator("","DefinitionOfRefferrers","تعریف معرفین",""),

                MenuCreator("","Settings","تنظیمات"),
                
                MenuCreator("","Support","پشتیبانی"),
                MenuCreator("","Ticket","تیکت",""),
                MenuCreator("","SupportContact","تماس با پشتیبانی",""),
            });
        }
    }
}
