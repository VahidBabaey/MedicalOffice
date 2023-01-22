﻿using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.ServiceFile.Requests.Commands;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ServiceController : Controller
{
    private readonly IMediator _mediator;

    public ServiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] ServiceDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddServiceCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteServiceCommand() { OfficeId = Guid.Parse(officeId), ServiceId = id });

        return Ok(response);
    }

    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateServiceDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditServiceCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<ServiceListDTO>>> GetAll([FromQuery] ListDto dto, Guid sectionId)
    {
        var response = await _mediator.Send(new GetAllServicesBySectionIDQuery() { DTO = dto, SectionId = sectionId });

        return Ok(response);
    }

    [HttpGet("Search")]
    public async Task<ActionResult<List<ServiceListDTO>>> GetSectionBySearch([FromQuery] string name, [FromQuery] string officeId, [FromQuery] string sectionId)
    {
        var response = await _mediator.Send(new GetServiceBySearchQuery() { Name = name, OfficeId = Guid.Parse(officeId), SectionId = Guid.Parse(sectionId) });

        return Ok(response);
    }
}