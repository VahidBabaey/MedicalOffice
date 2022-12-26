﻿using AutoMapper;
using MedicalOffice.Application.Dtos.PermissionDTO;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.BasicInfoListDTO;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.DrugIntractionDTO;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO;
using MedicalOffice.Application.Dtos.PictureDTO;
using MedicalOffice.Application.Dtos.RoleDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Application.Dtos.SpecializationDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;
using MedicalOffice.Application.Dtos.Reception;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;

namespace MedicalOffice.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientDTO>().ReverseMap();
        CreateMap<Patient, UpdateAddPatientDto>().ReverseMap();
        CreateMap<Patient, PatientListDto>().ConvertUsing(new PatientMapper());
        CreateMap<Patient, PatientListDto>().ReverseMap();
        CreateMap<MedicalStaff, PatientDTO>().ReverseMap();
        CreateMap<Section, SectionDTO>().ReverseMap();
        CreateMap<Section, SectionListDTO>().ReverseMap();
        CreateMap<Section, UpdateSectionDTO>().ReverseMap();
        CreateMap<Service, ServiceDTO>().ReverseMap();
        CreateMap<Service, UpdateServiceDTO>().ReverseMap();
        CreateMap<Service, ServiceListDTO>().ReverseMap();
        CreateMap<Service, ServiceListNameDTO>().ReverseMap();
        CreateMap<Tariff, ServiceTariffDTO>().ReverseMap();
        CreateMap<Insurance, InsuranceDTO>().ReverseMap();
        CreateMap<Insurance, UpdateInsuranceDTO>().ReverseMap();
        CreateMap<Insurance, InsuranceListDTO>().ReverseMap();
        CreateMap<Insurance, InsuranceNamesDTO>().ReverseMap();
        CreateMap<Specialization, SpecializationDTO>().ReverseMap();
        CreateMap<Specialization, SpecializationListDTO>().ReverseMap();
        CreateMap<Shift, ShiftDTO>().ReverseMap();
        CreateMap<Shift, UpdateShiftDTO>().ReverseMap();
        CreateMap<Shift, ShiftListDTO>().ReverseMap();
        CreateMap<Membership, MembershipDTO>().ReverseMap();
        CreateMap<Membership, MembershipListDTO>().ReverseMap();
        CreateMap<Membership, UpdateMembershipDTO>().ReverseMap();
        CreateMap<MemberShipService, MemberShipServiceDTO>().ReverseMap();
        CreateMap<DrugShape, DrugShapeListDTO>().ReverseMap();
        CreateMap<DrugSection, DrugSectionListDTO>().ReverseMap();
        CreateMap<DrugUsage, DrugUsageListDTO>().ReverseMap();
        CreateMap<DrugConsumption, DrugConsumptionListDTO>().ReverseMap();
        CreateMap<Drug, DrugDTO>().ReverseMap();
        CreateMap<Drug, UpdateDrugDTO>().ReverseMap();
        CreateMap<Drug, DrugListDTO>().ReverseMap();
        CreateMap<ExperimentPre, ExperimentDTO>().ReverseMap();
        CreateMap<ExperimentPre, UpdateExperimentDTO>().ReverseMap();
        CreateMap<ExperimentPre, ExperimentListDTO>().ReverseMap();
        CreateMap<DrugIntraction, DrugIntractionDTO>().ReverseMap();
        CreateMap<DrugIntraction, UpdateDrugIntractionDTO>().ReverseMap();
        CreateMap<DrugIntraction, DrugIntractionListDTO>().ReverseMap();
        CreateMap<Role, RoleListDTO>().ReverseMap();
        CreateMap<MedicalStaff, MedicalStaffDTO>().ReverseMap();
        CreateMap<MedicalStaff, UpdateMedicalStaffDTO>().ReverseMap();
        CreateMap<MedicalStaff, MedicalStaffListDTO>().ReverseMap();
        CreateMap<MedicalStaff, MedicalStaffNameListDTO>().ReverseMap();
        CreateMap<MedicalStaff, MedicalStaffNamesDTO>().ReverseMap();
        CreateMap<MedicalStaffSchedule, MedicalStaffScheduleDTO>().ReverseMap();
        CreateMap<MedicalStaffSchedule, MedicalStaffScheduleListDTO>().ReverseMap();
        CreateMap<BasicInfo, BasicInfoListDTO>().ReverseMap();
        CreateMap<BasicInfoDetail, BasicInfoDetailDTO>().ReverseMap();
        CreateMap<BasicInfoDetail, illnessNamesListDTO>()
            .ForMember(dest => dest.illnessName, opt => opt.MapFrom(src => src.InfoDetailName))
            .ReverseMap();
        CreateMap<BasicInfoDetail, CommitmentNamesListDTO>()
            .ForMember(dest => dest.CommitmentName, opt => opt.MapFrom(src => src.InfoDetailName))
            .ReverseMap();
        CreateMap<BasicInfoDetail, UpdateBasicInfoDetailDTO>().ReverseMap();
        CreateMap<BasicInfoDetail, BasicInfoDetailListDTO>().ReverseMap();
        CreateMap<PatientIllnessForm, PatientIllnessFormListDTO>().ReverseMap();
        CreateMap<PatientIllnessForm, PatientIllnessFormDTO>().ReverseMap();
        CreateMap<PatientReferralForm, PatientReferralFormDTO>().ReverseMap();
        CreateMap<PatientReferralForm, PatientReferralFormListDTO>().ReverseMap();
        CreateMap<PatientCommitmentForm, AddPatientCommitmentsFormDTO>().ReverseMap();
        CreateMap<PatientCommitmentForm, PatientCommitmentsFormListDTO>().ReverseMap();
        CreateMap<Permission, PermissionDTO>().ReverseMap();
        CreateMap<Permission, UpdatePermissionDTO>().ReverseMap();
        CreateMap<Permission, PermissionListDTO>().ReverseMap();
        CreateMap<FormCommitment, FormCommitmentDTO>().ReverseMap();
        CreateMap<FormCommitment, UpdateFormCommitmentDTO>().ReverseMap();
        CreateMap<FormCommitment, FormCommitmentListDTO>().ReverseMap();
        CreateMap<Picture, PictureUploadDTO>().ReverseMap();
        CreateMap<Picture, AddPictureDTO>().ReverseMap();
        CreateMap<Picture, PatientPicturesDTO>().ReverseMap();
        CreateMap<User, RegisterUserDTO>().ReverseMap();
        CreateMap<User, AuthenticatedUserDTO>().ReverseMap();
        CreateMap<User, MedicalStaffDTO>().ReverseMap();
        CreateMap<Membership, MembershipNamesDTO>().ReverseMap();
        CreateMap<ReceptionDiscount, ReceptionDiscountDTO>().ReverseMap();
        CreateMap<User, UserRoleDTO>().ReverseMap();
        CreateMap<Office, OfficeDTO>().ReverseMap();
        CreateMap<MedicalStaffSchedule, MedicalStaffDaySchedule>().ReverseMap();
        CreateMap<ServiceDuration, ServiceDurationDTO>().ReverseMap();
        CreateMap<AppointmentDetailsDTO, Appointment>().ReverseMap();
        CreateMap<Appointment, TransferAppointmentDTO>()
            .ReverseMap()
            .ForAllMembers(x=>x.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Appointment, AppointmentDescriptionDTO>().ReverseMap();
        CreateMap<Appointment, AppointmentTypeDTO>().ReverseMap();
        CreateMap<Appointment, AppointmentDTO>().ReverseMap();
    }

    public class PatientMapper : ITypeConverter<Patient, PatientListDto>
    {
        public PatientListDto Convert(Patient source, PatientListDto destination, ResolutionContext context)
        {
            destination = new()
            {
                Id = source.Id,
                BirthDate = source.BirthDate,
                FileNumber = source.FileNumber,
                Mobile = source.PatientContacts.Single(p => p.IsDefault).ContactValue,
                FatherName = source.FatherName,
                InsuranceId = source.InsuranceId,
                FirstName = source.FirstName,
                LastName = source.LastName
            };
            return destination;
        }
    }
}