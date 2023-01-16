using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.Experiment.Requests.Commands;
using MedicalOffice.Application.Features.Experiment.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExperimentController : Controller
{
    private readonly IMediator _mediator;

    public ExperimentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] ExperimentDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddExperimentCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteExperimentCommand() { ExperimentID = id, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateExperimentDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditExperimentCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<ExperimentListDTO>>> GetAll([FromQuery] ListDto dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllExperimentQuery() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
    [HttpGet("Search")]
    public async Task<ActionResult<List<ExperimentListDTO>>> GetExperimentBySearch([FromQuery] string name, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetExperimentBySearchQuery() { Name = name, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
}