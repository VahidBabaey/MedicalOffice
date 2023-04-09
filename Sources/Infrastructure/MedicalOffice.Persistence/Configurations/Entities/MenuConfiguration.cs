using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalOffice.Persistence.Configurations.Entities
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        private static Menu MenuCreator(string guidId, string name, string persianName, string? parentId = null, bool isActive = true, byte? order = null)
         => new() { Id = Guid.Parse(guidId), Name = name, PersianName = persianName, ParentId = parentId != null ? Guid.Parse(parentId) : null, IsActive = isActive, Order = order };

        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasData(new Menu[]
            {
                MenuCreator("b5e1e8df-35fb-4672-b7a2-12a30a4bd29e","BasicInfo","اطلاعات پایه",order:1),
                MenuCreator("7884aff9-de2c-410b-bfe0-43f510d378e3","OfficeInfo","اطلاعات مطب","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e",order:20),
                MenuCreator("2bcdcf7d-5830-431e-a343-ced19741d4a5","DefinitionOfMedicalStaffs","تعریف کادر درمان","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e",order:21),
                MenuCreator("c86bd8b9-f0d9-44db-9f45-5616218638ad","'MedicalStaffsSchedule","برنامه حضور کادر درمان","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e", order:22),
                MenuCreator("cfe66d95-299a-441b-b6b7-32b1c3993aa5","DefinitionOfSection","تعریف بخش","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e",order:23),
                MenuCreator("4d742e89-e8bc-44d1-ba16-f8326856264c","DefinitionOfServices","تعریف خدمات","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e",order:24),
                MenuCreator("2cf199d3-9361-4e7e-9cad-79f38c33a631","DefinitionOfShifts","تعریف شیفت","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e",order:25),
                MenuCreator("fb0ceffb-9b69-4811-8cf3-d159165fcb48","DefinitionOfMembership","تعریف عضویت","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e",order:26),
                MenuCreator("13bde77d-fe6f-4417-bd43-22ae27fed831","DefinitionOfInsurance","تعریف بیمه ها","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e",order:27),
                MenuCreator("5572e148-1703-47a3-ab9c-2ddd8b129d2e","DefinitionOfDrug","تعریف دارو","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e",isActive:false,order:28),
                MenuCreator("c7f11f6b-7490-4127-be99-46212d645b5a","DefinitionOfDrugInteractions","تعریف تداخلات دارو","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e",isActive : false, order:29),
                MenuCreator("36c67308-ccb3-4d0f-95b3-91593fa66463","DefinitionOfExperiment","تعریف آزمایش","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e",isActive : false, order:30),
                MenuCreator("ba6e3459-d759-421c-8975-5bca504f4db6","DefinitionOfThematicBase","تعریف اطلاعات پایه موضوعی","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e",order:31),
                MenuCreator("5cefe46e-bfea-4ca7-9a1b-347cdd5a4ef1","DefinitionOfRefferrers","تعریف معرفین","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e",isActive : false, order:32),
                MenuCreator("cad65760-68fc-43be-804c-4d22d957c887","DefinitionOfKCoefficient","تعریف ضریب K","b5e1e8df-35fb-4672-b7a2-12a30a4bd29e",isActive:false,order:33),

                MenuCreator("aaa52e09-ca9c-421a-972e-764ef9a22d4a","TodayPatient","بیماران امروز",isActive : false,order:2),

                MenuCreator("bd389ea9-3cd5-48d6-bf01-669f6a87711c","File","پرونده",order:3),
                MenuCreator("38c73f55-cc7b-49dd-a301-8dc441f0353c","CreateFile","تشکیل پرونده ","bd389ea9-3cd5-48d6-bf01-669f6a87711c",order:34),
                MenuCreator("38e6f085-be0e-446f-8ad8-ae2aa12fe332","AdvanceSearchFile","جستو جوی پیشرفته","bd389ea9-3cd5-48d6-bf01-669f6a87711c",order:35),

                MenuCreator("8018f694-2387-4e67-8263-1a994d010617","Reception","پذیرش",order:4),

                MenuCreator("8a239c9f-4943-44d5-affc-2836c8da52a6","Appointment","وقت دهی ",isActive : false, order:5),
                MenuCreator("fbcb96d0-5c1d-4e64-bc91-2863b8e1b98f","AppointmentSetting","تنظیمات وقت دهی","8a239c9f-4943-44d5-affc-2836c8da52a6",isActive : false, order:36),
                MenuCreator("24fee1ff-cb20-498d-bc82-5a5770c1534e","Appointment","وقت دهی","8a239c9f-4943-44d5-affc-2836c8da52a6", isActive : false, order : 37),

                MenuCreator("6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06","Visit","ویزیت",isActive:false,order:6),
                MenuCreator("a4568ac3-3157-49b2-95db-ae969c82b263","Visit","ویزیت","6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06", isActive : false,order:38),
                MenuCreator("9c711d69-5783-4586-a9a5-a7ce5a51de2a","PMH","PMH","6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06", isActive : false, order : 39),
                MenuCreator("35b49377-58ec-4312-b51c-5211df74b379","Paraclinic","پاراکلینیک","6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06", isActive : false, order : 40),
                MenuCreator("2ee4d34e-d3a6-4cdb-b28a-fe2cb2fbd8dc","Graphs","نمودارها","6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06", isActive : false, order : 41),
                MenuCreator("7e25e8ea-3591-4367-81a3-48389ebfe33c","Forms","فرم ها","6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06", isActive : false,order:42),
                MenuCreator("0d9065d4-d5bc-4705-8530-e703360b69e9","َAtFirstLook","در یک نگاه","6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06", isActive : false,order:43),
                MenuCreator("f0436c8d-0d2d-4b32-82a2-baac3a8f3d19","َElectronicPrescribing","نسخه نویسی الکترونیک","6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06", isActive : false,order:44),
                MenuCreator("03fc5e29-4d7f-4a45-a898-e8cac402e226","LightPen","قلم نوری","6a32cd92-f719-4ce0-b8c2-1e8b17ce5a06", isActive : false,order:45),

                MenuCreator("cc41f355-f2d3-445c-a03e-7936a26f1128","reports","گزارش ها", isActive : false,order:7),

                MenuCreator("d60cdae5-54a9-4924-af24-c29e5978f609","Warehousing","انبارداری", isActive : false,order:8),
                MenuCreator("8f3efe85-509c-4df7-9790-e8d0125c9344","DefinitionOfWarehousing","تعاریف انبار","d60cdae5-54a9-4924-af24-c29e5978f609", isActive : false,order:46),
                MenuCreator("337151c9-5b77-411a-bd25-be18663a00a5","EntranceRemittance","حواله ورود","d60cdae5-54a9-4924-af24-c29e5978f609", isActive : false,order:47),
                MenuCreator("3b876efe-46c6-45b6-bfe8-3969d939981e","ExitRemittance","حواله خروج","d60cdae5-54a9-4924-af24-c29e5978f609", isActive : false,order:48),
                MenuCreator("707c985a-4fcf-4e75-bc61-33e160c326f6","CirculationOfGoods","گردش کالا","d60cdae5-54a9-4924-af24-c29e5978f609", isActive : false,order:49),
                MenuCreator("a8220a4f-087f-476f-a07a-33c1fb45b15d","StockOfGoods","موجودی کالا","d60cdae5-54a9-4924-af24-c29e5978f609", isActive : false,order:50),
                MenuCreator("fb172545-8c71-4559-8b65-abdecbe7e644","StoreReports","گزارشات انبار","d60cdae5-54a9-4924-af24-c29e5978f609", isActive : false,order:51),

                MenuCreator("b809a0b1-15a4-492a-b3fe-929ff8470231","SMS","پیامک", isActive : false,order:9),
                MenuCreator("dca4d822-579d-4e31-b235-e7808faa804d","SMSSharge","شارژ پیامک","b809a0b1-15a4-492a-b3fe-929ff8470231", isActive : false,order:52),
                MenuCreator("58aa8309-ae51-4c1f-a427-d8a66d881f2a","Settings","تنظیمات","b809a0b1-15a4-492a-b3fe-929ff8470231", isActive : false,order:53),
                MenuCreator("45ca676b-e6e4-4457-9254-6674ac59f44c","Sents","ارسال شده ها","b809a0b1-15a4-492a-b3fe-929ff8470231", isActive : false,order:54),

                MenuCreator("d81547e7-2050-43b5-a127-6cbefb0d3580","Settings","تنظیمات",isActive:false,order:10),

                MenuCreator("f7829a47-bcd2-4ede-b3ba-2624222437cd","Support","پشتیبانی", isActive : false,order:11),
                MenuCreator("ea13c4f0-89f9-4d0d-b1aa-a8f7222600db","Ticket","تیکت","f7829a47-bcd2-4ede-b3ba-2624222437cd", isActive : false,order:55),
                MenuCreator("31535b28-a356-426a-b3ca-8605c13746f3","SupportContact","تماس با پشتیبانی","f7829a47-bcd2-4ede-b3ba-2624222437cd", isActive : false,order:56),
            });
        }
    }
}
