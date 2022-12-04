﻿using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public DbSet<Allergy> Allergies => Set<Allergy>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<AppointmentService> AppointmentServices => Set<AppointmentService>();
    public DbSet<AppointmentType> AppointmentTypes => Set<AppointmentType>();
    public DbSet<BasicInfo> BasicInfos => Set<BasicInfo>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<BasicInfoDetail> BasicInfoDetail => Set<BasicInfoDetail>();
    public DbSet<Diagnose> Diagnoses => Set<Diagnose>();
    public DbSet<DiscountType> DiscountTypes => Set<DiscountType>();
    public DbSet<FormCommitment> FormCommitments => Set<FormCommitment>();
    public DbSet<Drug> Drugs => Set<Drug>();
    public DbSet<DrugIntraction> DrugIntractions => Set<DrugIntraction>();
    public DbSet<DrugConsumption> DrugConsumptions => Set<DrugConsumption>();
    public DbSet<DrugUsage> DrugUsages => Set<DrugUsage>();
    public DbSet<DrugShape> DrugShapes => Set<DrugShape>();
    public DbSet<DrugSection> DrugSections => Set<DrugSection>();
    public DbSet<DrugAbuse> DrugAbuses => Set<DrugAbuse>();
    public DbSet<ExperimentPre> ExperimentPres => Set<ExperimentPre>();
    public DbSet<DrugPrescription> DrugPrescriptions => Set<DrugPrescription>();
    public DbSet<FDO> FDO => Set<FDO>();
    public DbSet<GeneralExamination> GeneralExaminations => Set<GeneralExamination>();
    public DbSet<ICD11> ICD11 => Set<ICD11>();
    public DbSet<Insurance> Insurances => Set<Insurance>();
    public DbSet<KMultiplier> KMultipliers => Set<KMultiplier>();
    public DbSet<KMultiplierDetail> KMultiplierDetails => Set<KMultiplierDetail>();
    public DbSet<MedicalAction> MedicalActions => Set<MedicalAction>();
    public DbSet<MedicalStaff> MedicalStaffs => Set<MedicalStaff>();
    public DbSet<MedicalStaffWorkHourProgram> MedicalStaffWorkHourPrograms => Set<MedicalStaffWorkHourProgram>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<MemberShipService> MemberShipServices => Set<MemberShipService>();
    public DbSet<Office> Offices => Set<Office>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Picture> Pictures => Set<Picture>();
    public DbSet<PatientCommitmentForm> PatientCommitmentForms => Set<PatientCommitmentForm>();
    public DbSet<PatientFiles> PatientFiless => Set<PatientFiles>();
    public DbSet<PatientIllnessForm> PatientIllnessForms => Set<PatientIllnessForm>();
    public DbSet<PatientReferralForm> PatientReferralForms => Set<PatientReferralForm>();
    public DbSet<PatientAddress> PatientAddresses => Set<PatientAddress>();
    public DbSet<PatientContact> PatientContacts => Set<PatientContact>();
    public DbSet<PatientTag> PatientTags => Set<PatientTag>();
    public DbSet<PhysicalExam> PhysicalExams => Set<PhysicalExam>();
    public DbSet<PMH> PMH => Set<PMH>();
    public DbSet<Reception> Receptions => Set<Reception>();
    public DbSet<ReceptionDetail> ReceptionDetails => Set<ReceptionDetail>();
    public DbSet<ReceptionDiscount> ReceptionDiscounts => Set<ReceptionDiscount>();
    public DbSet<ReceptionMedicalStaff> ReceptionMedicalStaffs => Set<ReceptionMedicalStaff>();
    public DbSet<RoutineMedication> RoutineMedications => Set<RoutineMedication>();
    public DbSet<RVU3> RVU3 => Set<RVU3>();
    public DbSet<Section> Sections => Set<Section>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Shift> Shifts => Set<Shift>();
    public DbSet<SNOMED> SNOMED => Set<SNOMED>();
    public DbSet<SocialHistory> SocialHistories => Set<SocialHistory>();
    public DbSet<Specialization> Specializations => Set<Specialization>();
    public DbSet<Tariff> Tariffs => Set<Tariff>();
    public DbSet<MedicalStaffRole> MedicalStaffRoles => Set<MedicalStaffRole>();
    public DbSet<UserOfficeRole> UserOfficeRoles => Set<UserOfficeRole>();
    public DbSet<UserOfficePermission> UserOfficePermissions => Set<UserOfficePermission>();
    public DbSet<MedicalStaffOfficeSpecialization> MedicalStaffOfficeSpecializations => Set<MedicalStaffOfficeSpecialization>();
    public DbSet<MedicalStaffServiceSharePercent> MedicalStaffServiceSharePercents => Set<MedicalStaffServiceSharePercent>();
    public DbSet<ServiceDuration> ServiceDurations=> Set<ServiceDuration>();

    private readonly IUserResolverService _userResolver;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserResolverService userResolver) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
        _userResolver = userResolver;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // https://learn.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?source=recommendations&view=aspnetcore-6.0#customize-the-model
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Role>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRole");

        modelBuilder.Entity<MedicalStaffWorkHourProgram>().HasKey(entity=>entity.Id);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userId = _userResolver.GetUserId().Result;

        foreach (var entry in ChangeTracker.Entries<IPrimaryKeyEntity<Guid>>())
        {
            if (entry.State == EntityState.Added && entry.Entity.Id == default)
                entry.Entity.Id = Guid.NewGuid();
        }

        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            entry.Entity.LastUpdatedDate = DateTime.Now;

            if (userId != null)
                entry.Entity.LastUpdatedById = Guid.Parse(userId);

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.Now;

                if (userId != null)
                    entry.Entity.CreatedById = Guid.Parse(userId);
            }

        }

        foreach (var entry in ChangeTracker.Entries<BaseDomainEntity<Guid>>())
        {
            entry.Entity.LastUpdatedDate = DateTime.Now;

            if (userId != null)
                entry.Entity.LastUpdatedById = Guid.Parse(userId);

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.Now;

                if (userId != null)
                    entry.Entity.CreatedById = Guid.Parse(userId);
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}