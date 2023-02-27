using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Command;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
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

    //[Authorize]
    [HttpGet("commitmentforms")]
    public async Task<ActionResult<List<CommitmentNamesListDTO>>> GetAllCommitmentForms([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllCommitmentsNamesFormQuery() { OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] AddPatientCommitmentsFormDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddPatientCommitmentFormCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpGet]
    public async Task<ActionResult<List<PatientCommitmentsFormListDTO>>> GetAll([FromQuery] ListDto dto, Guid patientid, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllPatientCommitmentsFormQuery() { DTO = dto, PatientId = patientid, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id)
    {
        var response = await _mediator.Send(new DeletePatientCommitmentFormCommand() { PatientCommitmentFormId = id });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}