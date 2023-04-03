using FluentValidation;
using MediatR;
using MedicalOffice.Application.Dtos.Identity.Validators;
using MedicalOffice.Application.Dtos.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MedicalOffice.Application.Dtos.IdentityDTO.Validators;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Dtos.OfficeDTO.Validators;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Dtos.ServiceDurationDTO.Validators;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO.Validators;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Validator;
using MedicalOffice.WebApi.WebApi.Controllers;
using MedicalOffice.Application.Dtos.MedicalStaffDTO.Validators;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO.Validator;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.CashDTO.Validators;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Dtos.DrugDTO.Validators;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.DrugIntractionDTO.Validator;
using MedicalOffice.Application.Dtos.DrugIntractionDTO;
using MedicalOffice.Application.Dtos.ExperimentDTO.Validators;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Dtos.FormCommitmentDTO.Validators;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.InsuranceDTO.Validators;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.MembershipDTO.Validators;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO.Validators;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;
using MedicalOffice.Application.Dtos.PatientDTO.Validators;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO.Validator;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO.Validator;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO.Validators;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Application.Dtos.SectionDTO.Validators;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ServiceDTO.Validators;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Dtos.ShiftDTO.Validators;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Application.Dtos.SpecializationDTO.Validators;
using MedicalOffice.Application.Dtos.SpecializationDTO;
using MedicalOffice.Application.Dtos.Tariff.Validators;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Application.Dtos.PictureDTO.Validator;
using MedicalOffice.Application.Dtos.PictureDTO;
using MedicalOffice.Application.Dtos.ServiceRoomDTO;
using MedicalOffice.Application.Dtos.ServiceRoomDTO.Validator;
using MedicalOffice.Domain;

namespace MedicalOffice.Application;

public static class ServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        #region Identity
        services.AddScoped<IValidator<RegisterUserWithoutPassDTO>, RegisterUserWithoutPassValidator>();
        services.AddScoped<IValidator<RegisterUserDTO>, RegisterUserValidator>();
        services.AddScoped<IValidator<AuthenticateByPasswordDTO>, AuthenticateByPasswordValidator>();
        services.AddScoped<IValidator<AuthenticateByTotpDTO>, AuthenticateByTotpValidator>();
        services.AddScoped<IValidator<UserStatusRequestDTO>, UserStatusRequestValidator>();
        services.AddScoped<IValidator<SetPasswordDTO>, SetPasswordValidator>();
        services.AddScoped<IValidator<ResetPasswordDTO>, ResetPasswordValidator>();
        services.AddScoped<IValidator<UpdateUserRoleDTO>, UpdateUserRoleValidator>();
        services.AddScoped<IValidator<SendTotpDTO>, SendTotpValidator>();
        services.AddScoped<IValidator<VerifyTotpDTO>, VerifyTotpValidator>();
        #endregion Identity

        #region Office
        services.AddScoped<IValidator<OfficeDTO>, OfficeDTOValidator>();
        services.AddScoped<IValidator<UserOfficeDTO>, UserOfficeValidator>();
        #endregion Office

        #region ServiceDuration
        services.AddScoped<IValidator<ServiceDurationDTO>, AddServiceDurationValidator>();
        services.AddScoped<IValidator<EditServiceDurationDTO>, EditServiceDurationValidator>();
        #endregion

        #region MedicalStaffSchedule
        services.AddScoped<IValidator<MedicalStaffScheduleDTO>, AddMedicalStaffScheduleValidator>();
        services.AddScoped<IValidator<MedicalStaffScheduleDTO>, EditMedicalStaffScheduleValidator>();
        services.AddScoped<IValidator<MedicalStaffDaySchedule>, MedicalStaffDayScheduleValidator>();
        #endregion

        #region Appointment
        services.AddScoped<IValidator<AddAppointmentDto>, AddAppointmentValidator>();
        services.AddScoped<IValidator<SearchAppointmentsDTO>, SearchAppointmentValidator>();
        services.AddScoped<IValidator<FilterFields>, FilterFieldsValidator>();
        services.AddScoped<IValidator<GetSpecificPeriodAppointmentDTO>, GetSpecificPeriodAppointmentValidator>();
        services.AddScoped<IValidator<UpdateAppointmentDescriptionDTO>, UpdateAppointmentDescriptionValidator>();
        services.AddScoped<IValidator<UpdateAppointmentTypeDTO>, AppointmentTypeValidator>();
        services.AddScoped<IValidator<TransferAppointmentDTO>, TransferAppointmentValidator>();
        services.AddScoped<IValidator<UpdateAppointmentPatientInfoDto>, UpdateAppointmnetPatientInfoValidator>();
        #endregion

        #region MedicalStaff
        services.AddScoped<IValidator<MedicalStaffDTO>, AddMedicalStaffValidator>();
        services.AddScoped<IValidator<UpdateMedicalStaffDTO>, UpdateMedicalStaffValidator>();
        #endregion

        #region Specialization
        services.AddScoped<IValidator<SpecializationDTO>, AddSpecializationValidator>();
        #endregion

        #region BasicInfo
        services.AddScoped<IValidator<BasicInfoDetailDTO>, AddBasicInfoDetailValidator>();
        services.AddScoped<IValidator<UpdateBasicInfoDetailDTO>, UpdateBasicInfoDetailValidator>();
        #endregion

        #region Section
        services.AddScoped<IValidator<AddSectionDTO>, AddSectionValidator>();
        services.AddScoped<IValidator<UpdateSectionDTO>, UpdateSectionValidator>();
        #endregion

        #region Service
        services.AddScoped<IValidator<ServiceDTO>, AddServiceValidator>();
        services.AddScoped<IValidator<UpdateServiceDTO>, UpdateServiceValidator>();
        #endregion

        #region Tariff
        services.AddScoped<IValidator<TariffDTO>, AddTariffValidator>();
        #endregion

        #region Insurance
        services.AddScoped<IValidator<InsuranceDTO>, AddInsuranceValidator>();
        services.AddScoped<IValidator<UpdateInsuranceDTO>, UpdateInsuranceValidator>();
        #endregion

        #region Shift
        services.AddScoped<IValidator<UpdateShiftDTO>, UpdateShiftValidator>();
        services.AddScoped<IValidator<ShiftDTO>, AddShiftValidator>();
        #endregion

        #region Membership
        services.AddScoped<IValidator<MembershipDTO>, AddMembershipValidator>();
        services.AddScoped<IValidator<UpdateMembershipDTO>, UpdateMembershipValidator>();
        #endregion

        #region Drug
        services.AddScoped<IValidator<DrugDTO>, AddDrugValidator>();
        services.AddScoped<IValidator<UpdateDrugDTO>, UpdateDrugValidator>();
        #endregion

        #region DrugIntraction
        services.AddScoped<IValidator<DrugIntractionDTO>, AddDrugIntractionValidator>();
        services.AddScoped<IValidator<UpdateDrugIntractionDTO>, UpdateDrugIntractionValidator>();
        #endregion

        #region Experiment
        services.AddScoped<IValidator<ExperimentDTO>, AddExperimentValidator>();
        services.AddScoped<IValidator<UpdateExperimentDTO>, UpdateExperimentValidator>();
        #endregion

        #region Patient
        services.AddScoped<IValidator<AddPatientDTO>, AddPatientValidator>();
        services.AddScoped<IValidator<UpdatePatientDTO>, UpdatePatientValidator>();
        services.AddScoped<IValidator<PatientReferralFormDTO>, AddPatientReferralFormValidator>();
        services.AddScoped<IValidator<PatientIllnessFormDTO>, AddPatientIllnessFormValidator>();
        services.AddScoped<IValidator<AddPatientCommitmentsFormDTO>, AddPatientCommitmentsFormValidator>();
        services.AddScoped<IValidator<PictureUploadDTO>, AddPictureValidator>();
        #endregion

        #region CashCart
        services.AddScoped<IValidator<CashCartDTO>, AddCashCartValidator>();
        services.AddScoped<IValidator<UpdateCashCartDTO>, UpdateCashCartValidator>();
        services.AddScoped<IValidator<CashCheckDTO>, AddCashCheckValidator>();
        services.AddScoped<IValidator<UpdateCashCheckDTO>, UpdateCashCheckValidator>();
        services.AddScoped<IValidator<CashPosDTO>, AddCashPosValidator>();
        services.AddScoped<IValidator<UpdateCashPosDTO>, UpdateCashPosValidator>();
        services.AddScoped<IValidator<CashesDTO>, AddCashValidator>();
        services.AddScoped<IValidator<CashMoneyDTO>, AddCashMoneyValidator>();
        #endregion

        #region MemberShipService
        services.AddScoped<IValidator<MemberShipServiceDTO>, AddMemberShipServiceValidator>();
        services.AddScoped<IValidator<UpdateMemberShipServiceDTO>, UpdateMemberShipServiceValidator>();
        #endregion

        #region ReceptionDetail
        services.AddScoped<IValidator<ReceptionDetailDTO>, AddReceptionDetailValidator>();
        services.AddScoped<IValidator<ReceptionsDTO>, AddReceptionValidator>();
        services.AddScoped<IValidator<UpdateReceptionDetailDTO>, UpdateReceptionDetailValidator>();
        services.AddScoped<IValidator<ReceptionDiscountDTO>, AddReceptionDiscountValidator>();
        #endregion

        #region ServiceRoom
        services.AddScoped<IValidator<AddServiceRoomDTO>, AddServiceRoomValidator>();
        services.AddScoped<IValidator<UpdateServiceRoomDTO>, UpdateServiceRoomValidator>();
        services.AddScoped<IValidator<ServiceRoomIdsDTO>, ServiceRoomIdsValidator>();
        #endregion
        return services;
    }
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services.AddMediatR(assembly);
    }
}