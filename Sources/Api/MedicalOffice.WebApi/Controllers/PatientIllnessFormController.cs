using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Command;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientIllnessFormController : Controller
{
    private readonly IMediator _mediator;

    public PatientIllnessFormController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet("illnessReasons")]
    public async Task<ActionResult<List<illnessNamesListDTO>>> GetPatientIllnessReasons()
    {
        var response = await _mediator.Send(new GetAlliillnessReasonsForillnessFormQuery());

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] PatientIllnessFormDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddPatientIllnessFormCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<PatientIllnessFormListDTO>>> GetAll([FromQuery] ListDto dto, Guid patientid)
    {
        var response = await _mediator.Send(new GetAllPatientIllnessFormQuery() { DTO = dto, PatientId = patientid });

        return Ok(response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id)
    {
        var response = await _mediator.Send(new DeletePatientIllnessFormCommand() { PatientIllnessFormId = id });

        return Ok(response);
    }
}