using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PermissionDTO.Validators;
using MedicalOffice.Application.Features.PermissionFile.Requests.Commands;
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

    public class AddPermissionCommandHandler : IRequestHandler<AddPermissionCommand, BaseCommandResponse>
    {
        private readonly IPermissionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddPermissionCommandHandler(IPermissionRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(AddPermissionCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            AddPermissionValidator validator = new();

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
                    var id = _repository.GetId(Guid.Parse(request.MedicalStaffid));
                    if (id == null)
                    {
                        throw new NullReferenceException("MedicalStaff Not found");
                    }
                    else
                    {
                    request.DTO.UserOfficeRoleId = id;
                    if (request.DTO.IsReceptionPermissionActive == false)
                    {
                        request.DTO.ReceptionDateChange = false;
                        request.DTO.ReceptionReturnregistration = false;
                        request.DTO.ReceptionDebtRegistration = false;
                        request.DTO.ReceptionEdit = false;
                        request.DTO.ReceptionDelete = false;
                        request.DTO.ReceptionShiftChange = false;
                    }
                    if (request.DTO.IsFilePermissionActive == false)
                    {
                        request.DTO.FileEdit = false;
                        request.DTO.FileDelete = false;
                        request.DTO.FileRegistration = false;
                        request.DTO.FileRegistrationAdvancePayment = false;
                        request.DTO.FileChangeDateAdvancePayment = false;
                        request.DTO.FileExcel = false;
                        request.DTO.FileChangeMedicalStaff = false;
                        request.DTO.FilePermissionPatientNumber = false;
                    }
                    if (request.DTO.IsDoctorPermissionActive == false)
                    {
                        request.DTO.DoctorVisitRegistration = false;
                        request.DTO.DoctorVisitEdit = false;
                        request.DTO.DoctorVisitDelete = false;
                        request.DTO.DoctorPermissionPatientHistory = false;
                        request.DTO.DoctorPermissionLightPen = false;
                        request.DTO.DoctorPermissionPictures = false;
                        request.DTO.DoctorPermissionCommitments = false;
                        request.DTO.DoctorChangeOthersVisit = false;
                        request.DTO.DoctorPermissionForms = false;
                        request.DTO.DoctorPermissionPrescription = false;
                    }
                    if (request.DTO.IsReportPermissionActive == false)
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
                    if (request.DTO.IsStorePermissionActive == false)
                    {
                        request.DTO.StoreComidity = false;
                        request.DTO.StoreConsumerRegitration = false;
                        request.DTO.StoreComidityTrasportation = false;
                        request.DTO.StoreRemittanceRegitration = false;
                        request.DTO.StoreRemittanceEdit = false;
                        request.DTO.StoreRemittanceDelete = false;
                    }
                    if (request.DTO.IsTimingPermissionActive == false)
                    {
                        request.DTO.TimingRegistration = false;
                        request.DTO.TimingDelete = false;
                        request.DTO.TimingCancelation = false;
                        request.DTO.TimingRegistrationforSelectedDoctors = false;
                    }
                    if (request.DTO.IsBasicInfoPermissionActive == false)
                    {
                        request.DTO.IsBasicInfoPermissionActive = false;
                    }
                    if (request.DTO.IsDashboardPermissionActive == false)
                    {
                        request.DTO.IsDashboardPermissionActive = false;
                    }
                    var Permission = _mapper.Map<Permission>(request.DTO);

                    Permission = await _repository.Add(Permission);
                    response.Success = true;
                    response.Message = $"{_requestTitle} succeded";
                    response.Data.Add(new { Id = Permission.Id });

                    log.Type = LogType.Success;
                    }

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
