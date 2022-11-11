using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PermissionFile.Requests.Commands;
using MedicalOffice.Application.Features.DrugFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PermissionFile.Handlers.Commands
{
    public class EditPermissionCommandHandler : IRequestHandler<EditPermissionCommand, BaseCommandResponse>
    {
        private readonly IPermissionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditPermissionCommandHandler(IPermissionRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(EditPermissionCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            Log log = new();

            try
            {
                if (request.DTOUp.IsReceptionPermissionActive == false)
                {
                    request.DTOUp.ReceptionDateChange = false;
                    request.DTOUp.ReceptionReturnregistration = false;
                    request.DTOUp.ReceptionDebtRegistration = false;
                    request.DTOUp.ReceptionEdit = false;
                    request.DTOUp.ReceptionDelete = false;
                    request.DTOUp.ReceptionShiftChange = false;
                }
                if (request.DTOUp.IsFilePermissionActive == false)
                {
                    request.DTOUp.FileEdit = false;
                    request.DTOUp.FileDelete = false;
                    request.DTOUp.FileRegistration = false;
                    request.DTOUp.FileRegistrationAdvancePayment = false;
                    request.DTOUp.FileChangeDateAdvancePayment = false;
                    request.DTOUp.FileExcel = false;
                    request.DTOUp.FileChangeUser = false;
                    request.DTOUp.FilePermissionPatientNumber = false;
                }
                if (request.DTOUp.IsDoctorPermissionActive == false)
                {
                    request.DTOUp.DoctorVisitRegistration = false;
                    request.DTOUp.DoctorVisitEdit = false;
                    request.DTOUp.DoctorVisitDelete = false;
                    request.DTOUp.DoctorPermissionPatientHistory = false;
                    request.DTOUp.DoctorPermissionLightPen = false;
                    request.DTOUp.DoctorPermissionPictures = false;
                    request.DTOUp.DoctorPermissionCommitments = false;
                    request.DTOUp.DoctorChangeOthersVisit = false;
                    request.DTOUp.DoctorPermissionForms = false;
                    request.DTOUp.DoctorPermissionPrescription = false;
                }
                if (request.DTOUp.IsReportPermissionActive == false)
                {
                    request.DTOUp.ReportDailyCash = false;
                    request.DTOUp.ReportFinancial = false;
                    request.DTOUp.ReportExpense = false;
                    request.DTOUp.ReportDebtors = false;
                    request.DTOUp.ReportDeposit = false;
                    request.DTOUp.ReportIntroducers = false;
                    request.DTOUp.ReportInstallment = false;
                    request.DTOUp.ReportElectronicPrescription = false;
                    request.DTOUp.ReportStatuseofPatients = false;
                    request.DTOUp.ReportServicesProvided = false;
                    request.DTOUp.ReportTimimg = false;
                    request.DTOUp.ReportDoctorsPerformancd = false;
                    request.DTOUp.ReportExpertsPerformancd = false;
                    request.DTOUp.ReportInsurances = false;
                    request.DTOUp.ReportInsuranceCopies = false;
                    request.DTOUp.ReportReturns = false;
                    request.DTOUp.ReportStaticticalVisits = false;
                    request.DTOUp.ReportSpecialForms = false;
                    request.DTOUp.ReportStore = false;
                }
                if (request.DTOUp.IsStorePermissionActive == false)
                {
                    request.DTOUp.StoreComidity = false;
                    request.DTOUp.StoreConsumerRegitration = false;
                    request.DTOUp.StoreComidityTrasportation = false;
                    request.DTOUp.StoreRemittanceRegitration = false;
                    request.DTOUp.StoreRemittanceEdit = false;
                    request.DTOUp.StoreRemittanceDelete = false;
                }
                if (request.DTOUp.IsTimingPermissionActive == false)
                {
                    request.DTOUp.TimingRegistration = false;
                    request.DTOUp.TimingDelete = false;
                    request.DTOUp.TimingCancelation = false;
                    request.DTOUp.TimingRegistrationforSelectedDoctors = false;
                }
                if (request.DTOUp.IsBasicInfoPermissionActive == false)
                {
                    request.DTOUp.IsBasicInfoPermissionActive = false;
                }
                if (request.DTOUp.IsDashboardPermissionActive == false)
                {
                    request.DTOUp.IsDashboardPermissionActive = false;
                }
                var Permission = _mapper.Map<Permission>(request.DTOUp);

                await _repository.Update(Permission);

                response.Success = true;
                response.Message = $"{_requestTitle} succeded";
                response.Data.Add(new { Id = Permission.Id });

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
                response.Message = $"{_requestTitle} failed";
                response.Errors.Add(error.Message);

                log.Type = LogType.Error;
            }


            log.Header = response.Message;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
