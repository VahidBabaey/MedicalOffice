﻿using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Membership;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Command;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Commands;
using MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientReferralFormController : Controller
{
    private readonly IMediator _mediator;

    public PatientReferralFormController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("GetPatientIllnessReasons")]
    public async Task<ActionResult<List<BasicInfoDetailListDTO>>> GetPatientIllnessReasons()
    {
        var response = await _mediator.Send(new GetAlliillnessReasonsQuery());

        return Ok(response);
    }
    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> Create([FromBody] PatientReferralFormDTO dto)
    {
        var response = await _mediator.Send(new AddPatientReferralFormCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpGet("Get")]
    public async Task<ActionResult<List<PatientReferralFormListDTO>>> GetAll([FromQuery] ListDto dto, Guid patientid)
    {
        var response = await _mediator.Send(new GetAllPatientReferralFormQuery() { DTO = dto, PatientId = patientid });

        return Ok(response);
    }
    [HttpDelete("Delete")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeletePatientReferralFormCommand() { PatientReferralFormId = id });

        return Ok(response);
    }

}