using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.AccessFile.Requests.Commands;
using MedicalOffice.Application.Features.DrugFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AccessFile.Handlers.Commands
{
    public class EditAccessCommandHandler : IRequestHandler<EditAccessCommand, BaseCommandResponse>
    {
        private readonly IAccessRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditAccessCommandHandler(IAccessRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(EditAccessCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            Log log = new();

            try
            {
                if (request.DTOUp.IsReceptionAccessActive == false)
                {
                    request.DTOUp.ReceptionDateChange = false;
                    request.DTOUp.ReceptionReturnregistration = false;
                    request.DTOUp.ReceptionDebtRegistration = false;
                    request.DTOUp.ReceptionEdit = false;
                    request.DTOUp.ReceptionDelete = false;
                    request.DTOUp.ReceptionShiftChange = false;
                }
                if (request.DTOUp.IsFileAccessActive == false)
                {
                    request.DTOUp.FileEdit = false;
                    request.DTOUp.FileDelete = false;
                    request.DTOUp.FileRegistration = false;
                    request.DTOUp.FileRegistrationAdvancePayment = false;
                    request.DTOUp.FileChangeDateAdvancePayment = false;
                    request.DTOUp.FileExcel = false;
                    request.DTOUp.FileChangeUser = false;
                    request.DTOUp.FileAccessPatientNumber = false;
                }
                if (request.DTOUp.IsDoctorAccessActive == false)
                {
                    request.DTOUp.DoctorVisitRegistration = false;
                    request.DTOUp.DoctorVisitEdit = false;
                    request.DTOUp.DoctorVisitDelete = false;
                    request.DTOUp.DoctorAccessPatientHistory = false;
                    request.DTOUp.DoctorAccessLightPen = false;
                    request.DTOUp.DoctorAccessPictures = false;
                    request.DTOUp.DoctorAccessCommitments = false;
                    request.DTOUp.DoctorChangeOthersVisit = false;
                    request.DTOUp.DoctorAccessForms = false;
                    request.DTOUp.DoctorAccessPrescription = false;
                }
                if (request.DTOUp.IsReportAccessActive == false)
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
                if (request.DTOUp.IsStoreAccessActive == false)
                {
                    request.DTOUp.StoreComidity = false;
                    request.DTOUp.StoreConsumerRegitration = false;
                    request.DTOUp.StoreComidityTrasportation = false;
                    request.DTOUp.StoreRemittanceRegitration = false;
                    request.DTOUp.StoreRemittanceEdit = false;
                    request.DTOUp.StoreRemittanceDelete = false;
                }
                if (request.DTOUp.IsTimingAccessActive == false)
                {
                    request.DTOUp.TimingRegistration = false;
                    request.DTOUp.TimingDelete = false;
                    request.DTOUp.TimingCancelation = false;
                    request.DTOUp.TimingRegistrationforSelectedDoctors = false;
                }
                if (request.DTOUp.IsBasicInfoAccessActive == false)
                {
                    request.DTOUp.IsBasicInfoAccessActive = false;
                }
                if (request.DTOUp.IsDashboardAccessActive == false)
                {
                    request.DTOUp.IsDashboardAccessActive = false;
                }
                var access = _mapper.Map<Access>(request.DTOUp);

                await _repository.Update(access);

                response.Success = true;
                response.Message = $"{_requestTitle} succeded";
                response.Data.Add(new { Id = access.Id });

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
            log.Messages = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
