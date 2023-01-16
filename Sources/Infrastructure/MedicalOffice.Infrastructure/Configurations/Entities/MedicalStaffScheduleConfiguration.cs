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
    public class MedicalStaffScheduleConfiguration : BaseEntityTypeConfiguration<MedicalStaffSchedule, Guid>
    {
        public override void ConfigureEntity(EntityTypeBuilder<MedicalStaffSchedule> builder)
        {
            builder
                .HasOne(x=>x.MedicalStaff)
                .WithMany(x=>x.MedicalStaffSchedules)
                .HasForeignKey(x=>x.MedicalStaffId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasData(new MedicalStaffSchedule[]
                    {
                        new MedicalStaffSchedule{
                            Id = Guid.Parse("cde5859d-3a54-4fa0-93b1-42ca4a574fd7"),
                            MedicalStaffId= Guid.Parse("803224e8-efc5-4998-b602-08dae7043559"),
                            MaxAppointmentCount= 10,
                            WeekDay= 0,
                            MorningStart= "07:00",
                            MorningEnd= "12:00",
                            EveningStart= "14:00",
                            EveningEnd="17:00"
                        }
                    });
        }
    }
}
