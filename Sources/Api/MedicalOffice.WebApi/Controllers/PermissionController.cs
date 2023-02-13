﻿using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.PermissionDTO;
using MedicalOffice.Application.Features.DrugFile.Requests.Queries;
using MedicalOffice.Application.Features.PermissionFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionController : Controller
{
    private readonly IMediator _mediator;

    public PermissionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<PermissionListDto>>> GetAll()
    {
        var response = await _mediator.Send(new GetPermissionsQuery() { });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> ChangeStaffPermission([FromQuery] string officeId, [FromQuery] string staffId, [FromBody] List<PermissionDto> dto)
    {
        var response = await _mediator.Send(new AddStaffPermissionsCommand() { DTO = dto, officeId = Guid.Parse(officeId), staffId = Guid.Parse(staffId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}