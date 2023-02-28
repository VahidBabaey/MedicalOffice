using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Application.Features.PatientFile.Requests.Commands;
using MedicalOffice.Application.Features.PatientFile.Requests.Queries;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : Controller
{
    private readonly IMediator _mediator;

    public PatientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] PatientDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddPatientCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<ActionResult<Guid>> Remove(Guid patientId, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeletePatientCommand() { PatientId = patientId, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdatePatientDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditPatientCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<PatientListDTO>>> GetAll([FromQuery] string officeId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllPatientsQuery() { Dto = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("SearchByRequestedFeilds")]
    public async Task<ActionResult<List<PatientListDTO>>> GetBySearch([FromQuery] SearchFields searchFields, [FromQuery] ListDto dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetPatientBySearchQuery() { Dto = dto, searchFields = searchFields, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    [Authorize]
    [HttpGet("searchreceptionslist")]
    public async Task<ActionResult<List<ReceptionListDTO>>> GetReceptionsList([FromQuery] string patientId)
    {
        var response = await _mediator.Send(new GetAllPatientReceptionsQuery() { PatientId = Guid.Parse(patientId) });

        return Ok(response);
    }
    [Authorize]
    [HttpGet("searchreceptiondetailsforreceptionlist")]
    public async Task<ActionResult<List<ReceptionDetailListForReceptionDTO>>> GetReceptionDetailsListForReception([FromQuery] string patientId, [FromQuery] string receptionId)
    {
        var response = await _mediator.Send(new GetAllPatientReceptionDetailsforReceptionQuery() { PatientId = Guid.Parse(patientId), ReceptionId = Guid.Parse(receptionId) });

        return Ok(response);
    }
}