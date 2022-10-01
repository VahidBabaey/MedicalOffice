using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AccessDTO.Validators;
using MedicalOffice.Application.Features.AccessFile.Requests.Commands;
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

    public class AddAccessCommandHandler : IRequestHandler<AddAccessCommand, BaseCommandResponse>
    {
        private readonly IAccessRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddAccessCommandHandler(IAccessRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(AddAccessCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            AddAccessValidator validator = new();

            Log log = new();

            var validationResult = await validator.ValidateAsync(request.DTO, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = $"{_requestTitle} failed";
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                log.Type = LogType.Error;
            }
            else
            {
                try
                {
                    string id = _repository.GetId(Guid.Parse(request.userid)).ToString();

                    request.DTO.UserOfficeRoleId = Guid.Parse(id);
                    if (request.DTO.IsReceptionAccessActive == false)
                    {
                        request.DTO.ReceptionDateChange = false;
                        request.DTO.ReceptionReturnregistration = false;
                        request.DTO.ReceptionDebtRegistration = false;
                        request.DTO.ReceptionEdit = false;
                        request.DTO.ReceptionDelete = false;
                        request.DTO.ReceptionShiftChange = false;
                    }
                    if (request.DTO.IsFileAccessActive == false)
                    {
                        request.DTO.FileEdit = false;
                        request.DTO.FileDelete = false;
                        request.DTO.FileRegistration = false;
                        request.DTO.FileRegistrationAdvancePayment = false;
                        request.DTO.FileChangeDateAdvancePayment = false;
                        request.DTO.FileExcel = false;
                        request.DTO.FileChangeUser = false;
                        request.DTO.FileAccessPatientNumber = false;
                    }
                    if (request.DTO.IsDoctorAccessActive == false)
                    {
                        request.DTO.DoctorVisitRegistration = false;
                        request.DTO.DoctorVisitEdit = false;
                        request.DTO.DoctorVisitDelete = false;
                        request.DTO.DoctorAccessPatientHistory = false;
                        request.DTO.DoctorAccessLightPen = false;
                        request.DTO.DoctorAccessPictures = false;
                        request.DTO.DoctorAccessCommitments = false;
                        request.DTO.DoctorChangeOthersVisit = false;
                        request.DTO.DoctorAccessForms = false;
                        request.DTO.DoctorAccessPrescription = false;
                    }
                    if (request.DTO.IsReportAccessActive == false)
                    {
                        request.DTO.ReportDailyCash = false;
                        request.DTO.ReportFinancial = false;
                        request.DTO.ReportExpense = false;
                        request.DTO.ReportDebtors = false;
                        request.DTO.ReportDeposit = false;
                        request.DTO.ReportIntroducers = false;
                        request.DTO.ReportInstallment = false;
                        request.DTO.ReportElectronicPrescription = false;
                        request.DTO.ReportStatuseofPatients = false;
                        request.DTO.ReportServicesProvided = false;
                        request.DTO.ReportTimimg = false;
                        request.DTO.ReportDoctorsPerformancd = false;
                        request.DTO.ReportExpertsPerformancd = false;
                        request.DTO.ReportInsurances = false;
                        request.DTO.ReportInsuranceCopies = false;
                        request.DTO.ReportReturns = false;
                        request.DTO.ReportStaticticalVisits = false;
                        request.DTO.ReportSpecialForms = false;
                        request.DTO.ReportStore = false;
                    }
                    if (request.DTO.IsStoreAccessActive == false)
                    {
                        request.DTO.StoreComidity = false;
                        request.DTO.StoreConsumerRegitration = false;
                        request.DTO.StoreComidityTrasportation = false;
                        request.DTO.StoreRemittanceRegitration = false;
                        request.DTO.StoreRemittanceEdit = false;
                        request.DTO.StoreRemittanceDelete = false;
                    }
                    if (request.DTO.IsTimingAccessActive == false)
                    {
                        request.DTO.TimingRegistration = false;
                        request.DTO.TimingDelete = false;
                        request.DTO.TimingCancelation = false;
                        request.DTO.TimingRegistrationforSelectedDoctors = false;
                    }
                    if (request.DTO.IsBasicInfoAccessActive == false)
                    {
                        request.DTO.IsBasicInfoAccessActive = false;
                    }
                    if (request.DTO.IsDashboardAccessActive == false)
                    {
                        request.DTO.IsDashboardAccessActive = false;
                    }
                    var access = _mapper.Map<Access>(request.DTO);

                    access = await _repository.Add(access);
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
            }

            log.Header = response.Message;
            log.Messages = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }

}
