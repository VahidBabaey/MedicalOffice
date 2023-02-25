using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO;
using MedicalOffice.Application.Features.PatientReferralFormFile.Request.Query;
using MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Commands;
using MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
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

    //[Authorize]
    [HttpGet("illnessReasons")]
    public async Task<ActionResult<List<illnessNamesListDTO>>> GetPatientIllnessReasons()
    {
        var response = await _mediator.Send(new GetAlliillnessReasonsForReferalFormQuery());

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] PatientReferralFormDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddPatientReferralFormCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpGet]
    public async Task<ActionResult<List<PatientReferralFormListDTO>>> GetAll([FromQuery] ListDto dto, Guid patientid, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllPatientReferralFormQuery() { DTO = dto, PatientId = patientid, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeletePatientReferralFormCommand() { PatientReferralFormId = id });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}