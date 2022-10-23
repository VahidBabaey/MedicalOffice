﻿using AutoMapper;
using MedicalOffice.Application.Dtos.PermissionDTO;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.BasicInfoListDTO;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.DrugIntractionDTO;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.UserDTO;
using MedicalOffice.Application.Dtos.UserWorkHoursProgramFileDTO;
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
using System.Security.Permissions;
using System.Data.Entity;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;

namespace MedicalOffice.Application.Profiles;

public class MappingProfile : Profile 
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientDTO>().ReverseMap();
        CreateMap<Patient, UpdateAddPatientDto>().ReverseMap();
        CreateMap<Patient, PatientListDto>().ReverseMap();
        CreateMap<User, PatientDTO>().ReverseMap();
        CreateMap<Section, SectionDTO>().ReverseMap();
        CreateMap<Section, SectionListDTO>().ReverseMap();
        CreateMap<Section, UpdateSectionDTO>().ReverseMap();
        CreateMap<Service, ServiceDTO>().ReverseMap();
        CreateMap<Service, UpdateServiceDTO>().ReverseMap();
        CreateMap<Service, ServiceListDTO>().ReverseMap();
        CreateMap<Service, ServiceListNameDTO>().ReverseMap();
        CreateMap<Insurance, InsuranceDTO>().ReverseMap();
        CreateMap<Insurance, UpdateInsuranceDTO>().ReverseMap();
        CreateMap<Insurance, InsuranceListDTO>().ReverseMap();
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
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, UpdateUserDTO>().ReverseMap();
        CreateMap<User, UserListDTO>().ReverseMap();
        CreateMap<User, UserNameListDTO>().ReverseMap();
        CreateMap<UserWorkHourProgram, UserWorkHoursProgramDTO>().ReverseMap();
        CreateMap<UserWorkHourProgram, UserWorkHoursProgramListDTO>().ReverseMap();
        CreateMap<BasicInfo, BasicInfoListDTO>().ReverseMap();
        CreateMap<BasicInfoDetail, BasicInfoDetailDTO>().ReverseMap();
        CreateMap<BasicInfoDetail, UpdateBasicInfoDetailDTO>().ReverseMap();
        CreateMap<BasicInfoDetail, BasicInfoDetailListDTO>().ReverseMap();
        CreateMap<PatientIllnessForm, PatientIllnessFormListDTO>().ReverseMap();
        CreateMap<PatientIllnessForm, PatientIllnessFormDTO>().ReverseMap();
        CreateMap<PatientReferralForm, PatientReferralFormDTO>().ReverseMap();
        CreateMap<PatientReferralForm, PatientReferralFormListDTO>().ReverseMap();
        CreateMap<Permission, PermissionDTO>().ReverseMap();
        CreateMap<Permission, UpdatePermissionDTO>().ReverseMap();
        CreateMap<Permission, PermissionListDTO>().ReverseMap();
        CreateMap<FormCommitment, FormCommitmentDTO>().ReverseMap();
        CreateMap<FormCommitment, UpdateFormCommitmentDTO>().ReverseMap();
        CreateMap<FormCommitment, FormCommitmentListDTO>().ReverseMap();
        CreateMap<Picture, PictureUploadDTO>().ReverseMap();
        CreateMap<Picture, AddPictureDTO>().ReverseMap();
        CreateMap<Picture, PatientPicturesDTO>().ReverseMap();
        CreateMap<Patient, PatientListDto>().ConvertUsing(new PatientMapper());
    }

    public class PatientMapper : ITypeConverter<Patient, PatientListDto>
    {
        public PatientListDto Convert(Patient source, PatientListDto destination, ResolutionContext context)
        {
            destination = new();
            destination.Id = source.Id;
            destination.BirthDate = source.BirthDate;
            destination.FileNumber = source.FileNumber;
            destination.Mobile = source.PatientContacts.Single(p => p.IsDefault).ContactValue;
            destination.FatherName = source.FatherName;
            destination.InsuranceId = source.InsuranceId;
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            return destination;
        }
    }
    //public class DrugIntractionMapper : ITypeConverter<DrugIntraction, DrugIntractionListDTO>
    //{

    //    public DrugIntractionListDTO Convert(DrugIntraction source, DrugIntractionListDTO destination, ResolutionContext context)
    //    {
    //        destination = new();
    //        destination.Id = source.Id;
    //        destination.Group1 = source.Group1;
    //        destination.Group2 = source.Group2;
    //        destination.Method = source.Method;
    //        destination.Effects = source.Effects;
    //        destination.Control = source.Control;
    //        destination.PDrugId = source.PDrugId;
    //        destination.PDrugName = _dbContext.Drugs.Select(q => new { q.Id, q.Name }).Where(q => q.Id == source.PDrugId).FirstOrDefault().Name,
    //    }
    //}
}