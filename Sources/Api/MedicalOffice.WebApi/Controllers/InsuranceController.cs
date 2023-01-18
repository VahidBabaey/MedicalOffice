﻿using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Commands;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InsuranceController : Controller
{
    private readonly IMediator _mediator;

    public InsuranceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] InsuranceDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddInsuranceCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteInsuranceCommand() { InsuranceID = id, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateInsuranceDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditInsuranceCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<InsuranceListDTO>>> GetAll([FromQuery] ListDto dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllInsuranceQuery() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}