using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Features.PatientFile.Requests.Commands;
using MedicalOffice.Application.Features.PatientFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
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

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] PatientDTO dto)
    {
        var response = await _mediator.Send(new AddPatientCommand() { Dto = dto });

        return Ok(response);
    }
    [HttpDelete]
    public async Task<ActionResult<Guid>> Delete(Guid patientId)
    {
        var response = await _mediator.Send(new DeletePatientCommand() { PatientId = patientId });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateAddPatientDto dto)
    {
        var response = await _mediator.Send(new EditPatientCommand() { Dto = dto });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<PatientListDto>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllPatientsQuery() { Dto = dto });

        return Ok(response);
    }
    [HttpGet("Search")]
    public async Task<ActionResult<List<PatientListDto>>> GetBySearch([FromQuery] ListDto dto, [FromQuery] SearchFields searchFields)
    {
        var response = await _mediator.Send(new GetPatientBySearchQuery() { Dto = dto, searchFields = searchFields });

        return Ok(response);
    }
}