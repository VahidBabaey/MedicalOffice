﻿using AutoMapper;
using MedicalOffice.Application.Dtos.AccessDTO;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.BasicInfoListDTO;
using MedicalOffice.Application.Dtos.DrugD;
using MedicalOffice.Application.Dtos.DrugIntractionD;
using MedicalOffice.Application.Dtos.Experiment;
using MedicalOffice.Application.Dtos.Insurance;
using MedicalOffice.Application.Dtos.MedicalStaffdto;
using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFile;
using MedicalOffice.Application.Dtos.Membership;
using MedicalOffice.Application.Dtos.Patient;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO;
using MedicalOffice.Application.Dtos.Roledto;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Dtos.Service;
using MedicalOffice.Application.Dtos.Shift;
using MedicalOffice.Application.Dtos.Specialization;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PazireshDTO>().ReverseMap();
        CreateMap<Patient, PatientListDto>().ReverseMap();
        CreateMap<User, PazireshDTO>().ReverseMap();
        CreateMap<Section, SectionDTO>().ReverseMap();
        CreateMap<Section, MembershipListDTO>().ReverseMap();
        CreateMap<Service, ServiceDTO>().ReverseMap();
        CreateMap<Service, ServiceListDTO>().ReverseMap();
        CreateMap<Service, ServiceListNameDTO>().ReverseMap();
        CreateMap<Insurance, InsuranceDTO>().ReverseMap();
        CreateMap<Insurance, InsuranceListDTO>().ReverseMap();
        CreateMap<Specialization, SpecializationDTO>().ReverseMap();
        CreateMap<Specialization, SpecializationListDTO>().ReverseMap();
        CreateMap<Shift, ShiftDTO>().ReverseMap();
        CreateMap<Shift, ShiftListDTO>().ReverseMap();
        CreateMap<Membership, MembershipDTO>().ReverseMap();
        CreateMap<DrugShape, DrugShapeListDTO>().ReverseMap();
        CreateMap<DrugSection, DrugSectionListDTO>().ReverseMap();
        CreateMap<DrugUsage, DrugUsageListDTO>().ReverseMap();
        CreateMap<DrugConsumption, DrugConsumptionListDTO>().ReverseMap();
        CreateMap<Drug, DrugDTO>().ReverseMap();
        CreateMap<Drug, DrugListDTO>().ReverseMap();
        CreateMap<ExperimentPre, ExperimentDTO>().ReverseMap();
        CreateMap<ExperimentPre, ExperimentListDTO>().ReverseMap();
        CreateMap<DrugIntraction, DrugIntractionDTO>().ReverseMap();
        CreateMap<DrugIntraction, DrugIntractionListDTO>().ReverseMap();
        CreateMap<Role, RoleListDTO>().ReverseMap();
        CreateMap<MedicalStaff, MedicalStaffDTO>().ReverseMap();
        CreateMap<MedicalStaff, MedicalStaffListDTO>().ReverseMap();
        CreateMap<MedicalStaff, MedicalStaffNameListDTO>().ReverseMap();
        CreateMap<MedicalStaffWorkHourProgram, MedicalStaffWorkHoursProgramDTO>().ReverseMap();
        CreateMap<MedicalStaffWorkHourProgram, MedicalStaffWorkHoursProgramListDTO>().ReverseMap();
        CreateMap<BasicInfo, BasicInfoListDTO>().ReverseMap();
        CreateMap<BasicInfoDetail, BasicInfoDetailDTO>().ReverseMap();
        CreateMap<BasicInfoDetail, BasicInfoDetailListDTO>().ReverseMap();
        CreateMap<PatientIllnessForm, PatientIllnessFormListDTO>().ReverseMap();
        CreateMap<PatientIllnessForm, PatientIllnessFormDTO>().ReverseMap();
        CreateMap<PatientReferralForm, PatientReferralFormDTO>().ReverseMap();
        CreateMap<PatientReferralForm, PatientReferralFormListDTO>().ReverseMap();
        CreateMap<Access, AccessDTO>().ReverseMap();
        CreateMap<Access, AccessListDTO>().ReverseMap();
    }
}