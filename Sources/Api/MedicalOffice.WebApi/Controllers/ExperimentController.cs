﻿using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.Experiment.Requests.Commands;
using MedicalOffice.Application.Features.Experiment.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] ExperimentDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddExperimentCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpDelete("list-experiment")]
    public async Task<IActionResult> RemoveList([FromBody] ExperimentListIDDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteExperimentListCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateExperimentDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditExperimentCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ExperimentListDTO>>> GetAll([FromQuery] string officeId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllExperimentQuery() {Dto = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("Search")]
    public async Task<ActionResult<List<ExperimentListDTO>>> GetExperimentBySearch([FromQuery] string name, [FromQuery] string officeId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetExperimentBySearchQuery() {Dto = dto, Name = name, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}