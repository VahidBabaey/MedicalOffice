using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Experiment;
using MedicalOffice.Application.Dtos.Membership;
using MedicalOffice.Application.Dtos.Section;
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
    public async Task<ActionResult<Guid>> Create([FromBody] ExperimentDTO dto)
    {
        var response = await _mediator.Send(new AddExperimentCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteExperimentCommand() { ExperimentID = id });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateExperimentDTO dto)
    {
        var response = await _mediator.Send(new EditExperimentCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<ExperimentListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllExperimentQuery() { DTO = dto });

        return Ok(response);
    }
}