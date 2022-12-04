﻿using MediatR;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Commands;

public class EditPatientCommand : IRequest<BaseResponse>
{
    //public Guid PatientId { get; set; }
    public UpdateAddPatientDto DTO { get; set; } = new UpdateAddPatientDto();
}
