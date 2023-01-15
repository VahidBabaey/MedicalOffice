using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Command;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientCommitmentForms : Controller
{
    private readonly IMediator _mediator;

    public PatientCommitmentForms(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Commitments")]
    public async Task<ActionResult<List<CommitmentNamesListDTO>>> GetAllCommitments()
    {
        var response = await _mediator.Send(new GetAllCommitmentsNamesFormQuery());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] AddPatientCommitmentsFormDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddPatientCommitmentFormCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<PatientCommitmentsFormListDTO>>> GetAll([FromQuery] ListDto dto, Guid patientid)
    {
        var response = await _mediator.Send(new GetAllPatientCommitmentsFormQuery() { DTO = dto, PatientId = patientid });

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeletePatientCommitmentFormCommand() { PatientCommitmentFormId = id });

        return Ok(response);
    }

}